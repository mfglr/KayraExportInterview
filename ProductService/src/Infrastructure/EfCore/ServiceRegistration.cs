using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application;
using ProductService.Domain;

namespace Infrastructure.EfCore
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddDbContext<ProductContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlServer")))
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
