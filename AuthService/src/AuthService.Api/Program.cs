using AuthService.Api.Auth;
using AuthService.Api.MassTransit;
using AuthService.Api.Middlewares;
using AuthService.Api.SerilogRegistration;
using AuthService.Application;
using AuthService.Domain;
using AuthService.Infrastructure;
using AuthService.Infrastructure.EfCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddCustomSerilog();

builder.Services.AddControllers();
builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
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
