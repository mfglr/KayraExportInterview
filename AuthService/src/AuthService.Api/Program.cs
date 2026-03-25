using AuthService.Api.Auth;
using AuthService.Api.MassTransit;
using AuthService.Api.Middlewares;
using AuthService.Application;
using AuthService.Domain;
using AuthService.Infrastructure;
using AuthService.Infrastructure.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services
    .AddAuthenticationAndAuthorization(builder.Configuration)
    .AddMassTransit(builder.Configuration)
    .AddDomain()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    DbInitializer.Init(scope.ServiceProvider);
}

app.UseExceptionHandler("/Error");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
