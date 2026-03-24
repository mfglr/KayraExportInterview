using Infrastructure.EfCore;
using MassTransit;
using Product.Worker.MassTransit;
using Product.Worker.MassTransit.Consumers.DeleteUserProducts_OnUserDeleted;

namespace Product.Worker.MassTransit
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var option = configuration.GetSection(nameof(MassTransitOptions)).Get<MassTransitOptions>()!;
            return services
                .AddMassTransit(
                    x =>
                    {
                        x.AddConsumer<DeleteUserProducts_OnUserDeleted_ProductService>();

                        x.AddEntityFrameworkOutbox<ProductContext>(o =>
                        {
                            o.UseSqlServer();
                            o.UseBusOutbox();
                        });

                        x.AddConfigureEndpointsCallback((context, name, cfg) =>
                        {
                            cfg.UseMessageRetry(r =>
                            {
                                r.Intervals(10, 50, 100, 1000, 1000, 1000, 1000, 1000);
                            });

                            cfg.UseEntityFrameworkOutbox<ProductContext>(context);
                        });

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host(option.Host, option.VirtualHost, h =>
                            {
                                h.Username(option.UserName);
                                h.Password(option.Password);
                            });
                            cfg.ConfigureEndpoints(context);
                        });
                    }
                );
        }
    }
}
