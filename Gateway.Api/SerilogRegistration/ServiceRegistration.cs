using Serilog;
using Serilog.Formatting.Json;

namespace Gateway.Api.SerilogRegistration
{
    public static class ServiceRegistration
    {
        public static void AddCustomSerilog(this WebApplicationBuilder builder)
        {
            var options = builder.Configuration.GetSection(nameof(RabbitMQOptions)).Get<RabbitMQOptions>()!;
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.RabbitMQ(
                    username: options.UserName,
                    password: options.Password,
                    hostnames: [options.Host],
                    vHost: options.VirtualHost,
                    
                    exchange: "LogExchange",
                    deliveryMode: Serilog.Sinks.RabbitMQ.RabbitMQDeliveryMode.Durable,
                    autoCreateExchange: true,
                    formatter: new JsonFormatter()
                )
                .CreateLogger();

            builder.Services.AddSerilog();
            builder.Host.UseSerilog();
        }
    }
}
