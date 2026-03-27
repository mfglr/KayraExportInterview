using Gateway.Api.Auth;
using Gateway.Api.RateLimiter;
using Gateway.Api.SerilogRegistration;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomSerilog();

builder.Services
    .AddCustomRateLimiting(builder.Configuration)
    .AddAuthenticationAndAuthorization(builder.Configuration)
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();
app.Run();
