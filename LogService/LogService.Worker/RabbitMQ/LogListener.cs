using Elastic.Clients.Elasticsearch;
using LogService.Application.Commands.CreateLog;
using LogService.Infractructure.ElasticSearch;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace LogService.Worker.RabbitMQ
{
    internal class LogListener(RabbitMQOptions options, IServiceProvider serviceProvider, ElasticsearchClient client) : BackgroundService
    {
        private readonly static string _queueName = "LogListener";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await MappingConfigurator.ConfigureAsync(client);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var factory = new ConnectionFactory
                    {
                        HostName = options.Host,
                        VirtualHost = options.VirtualHost,
                        UserName = options.UserName,
                        Password = options.Password,
                        AutomaticRecoveryEnabled = true,
                        TopologyRecoveryEnabled = true,
                        RequestedHeartbeat = TimeSpan.FromSeconds(3)
                    };

                    using var connection = await factory.CreateConnectionAsync(stoppingToken);
                    using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

                    await channel.ExchangeDeclareAsync(options.LogExchangeName, "fanout", durable: true, cancellationToken: stoppingToken);

                    await channel.QueueDeclareAsync(
                        queue: _queueName,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        cancellationToken: stoppingToken
                    );
                    await channel.QueueBindAsync(
                        queue: _queueName,
                        exchange: options.LogExchangeName,
                        routingKey: "",
                        cancellationToken: stoppingToken
                    );

                    var consumer = new AsyncEventingBasicConsumer(channel);
                    consumer.ReceivedAsync += async (model, @event) =>
                    {
                        var body = @event.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var request = JsonSerializer.Deserialize<CreateLogCommandRequest>(message)!;

                        await ConsumeAsync(request, stoppingToken);
                    };

                    await channel.BasicConsumeAsync(_queueName, autoAck: true, consumer: consumer, cancellationToken: stoppingToken);

                    await Task.Delay(Timeout.Infinite, stoppingToken);
                }
                catch
                {
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }

        private async Task ConsumeAsync(CreateLogCommandRequest request, CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(request, cancellationToken);
        }
    }
}
