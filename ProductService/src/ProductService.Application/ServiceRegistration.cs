using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Commands.CreateProduct;
using ProductService.Application.Commands.DeleteProduct;
using ProductService.Application.Commands.DeleteUserProducts;
using ProductService.Application.Commands.UpdateProduct;
using ProductService.Application.Queries;
using System.Reflection;

namespace ProductService.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton<CreateProductCommandMapper>()
                .AddSingleton<UpdateProductCommandMapper>()
                .AddSingleton<ProductQueryResponseMapper>()
                .AddSingleton<DeleteProductCommandMapper>()
                .AddSingleton<DeleteUserProductsCommandMapper>()
                .AddMediatR(
                    cfg => {
                        cfg.LicenseKey = configuration.GetSection("LuckPenny:LicenseKey").Value;
                        cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                    }
                )
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
    }
}
