using Elastic.Clients.Elasticsearch;
using LogService.Application;
using LogService.Infractructure;
using LogService.Infractructure.ElasticSearch;
using LogService.Worker.RabbitMQ;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddRabbitMQ(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrstructure(builder.Configuration);

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var client = scope.ServiceProvider.GetRequiredService<ElasticsearchClient>();
    await MappingConfigurator.Configure(client);
}

host.Run();
