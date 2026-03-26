using LogService.Application.Commands.CreateLog;
using LogService.Application.Commands.CreateLogs;
using LogService.Application.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LogService.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddSingleton<LogResponseMapper>()
                .AddSingleton<CreateLogCommandMapper>()
                .AddSingleton<CreateLogsCommandMapper>()
                .AddMediatR(
                    cfg =>
                    {
                        cfg.LicenseKey = configuration.GetSection("LuckPenny:LicenseKey").Value;
                        cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                    }
                );

    }
}
