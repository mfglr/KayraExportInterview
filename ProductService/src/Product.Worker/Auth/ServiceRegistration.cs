using Application;

namespace Product.Worker.Auth
{
    internal static class ServiceRegistration
    {
        public static IServiceCollection AddAuth(this IServiceCollection services) =>
            services
                .AddSingleton<IAuthService, ProductWorkerAuthService>();
    }
}
