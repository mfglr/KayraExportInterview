using Application.Commands.CreateProduct;
using Application.Commands.UpdateProduct;
using Application.Queries.SearchProduct;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton<CreateProductCommandMapper>()
                .AddSingleton<UpdateProductCommandMapper>()
                .AddSingleton<SearchProductQueryMapper>()
                .AddMediatR(
                    cfg => {
                        cfg.LicenseKey = configuration.GetSection("LuckPenny:LicenseKey").Value;
                        cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                    }
                )
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
    }
}
