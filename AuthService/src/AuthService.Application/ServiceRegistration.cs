using AuthService.Application.Commands.CreateUser;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuthService.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton<CreateUserCommandMapper>()
                .AddMediatR(
                    cfg =>
                    {
                        cfg.LicenseKey = configuration.GetSection("LuckPenny:LicenseKey").Value;
                        cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                    }
                )
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
    }
}
