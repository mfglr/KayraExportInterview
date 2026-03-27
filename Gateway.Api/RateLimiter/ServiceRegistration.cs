using System.Security.Claims;
using System.Threading.RateLimiting;

namespace Gateway.Api.RateLimiter
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddCustomRateLimiting(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddRateLimiter(options =>
                {
                    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
                        (context) =>
                        {
                            var key =
                                context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ??
                                context.Connection.RemoteIpAddress?.ToString() ??
                                "anonymous";

                            return RateLimitPartition.GetFixedWindowLimiter(key, _ => new FixedWindowRateLimiterOptions
                            {
                                PermitLimit = int.Parse(configuration["RateLimit:PermitLimit"]!),
                                Window = TimeSpan.FromSeconds(int.Parse(configuration["RateLimit:Window"]!)),
                                QueueLimit = int.Parse(configuration["RateLimit:QueueLimit"]!),
                                AutoReplenishment = true
                            });
                        }
                    );

                    options.OnRejected = async (context, token) =>
                    {
                        context.HttpContext.Response.StatusCode = 429;
                        await context.HttpContext.Response.WriteAsJsonAsync(new { Message = "Rate limit exceeded" }, token);
                    };

                });
    }
}
