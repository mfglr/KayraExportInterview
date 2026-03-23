using Application;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
