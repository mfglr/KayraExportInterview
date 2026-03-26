namespace LogService.Worker.RabbitMQ
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection(nameof(RabbitMQOptions)).Get<RabbitMQOptions>()!;
            return services
                .AddSingleton(options)
                .AddHostedService<LogListener>();
        }
    }
}
