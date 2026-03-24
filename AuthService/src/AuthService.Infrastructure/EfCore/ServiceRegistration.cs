using AuthService.Application;
using AuthService.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure.EfCore
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<AuthContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlServer")))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IUserRepository, UserRepository>();

            services
                .AddIdentityCore<User>(opt => {
                     opt.Password.RequireUppercase = false;
                     opt.Password.RequireLowercase = false;
                     opt.Password.RequireDigit = false;
                     opt.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthContext>();

            return services;
        }
            
    }
}
