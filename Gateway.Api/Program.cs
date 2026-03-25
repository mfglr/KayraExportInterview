using Gateway.Api.Auth;
using Gateway.Api.SerilogRegistration;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomSerilog();

builder.Services
    .AddAuthenticationAndAuthorization(builder.Configuration)
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseAuthentication();
app.MapReverseProxy();
app.Run();
