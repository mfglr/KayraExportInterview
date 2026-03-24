using Gateway.Api.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthenticationAndAuthorization(builder.Configuration)
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseAuthentication();
app.MapReverseProxy();
app.Run();
