using ProductService.Api.Auth;
using ProductService.Api.MassTransit;
using ProductService.Api.Middlewares;
using ProductService.Application;
using ProductService.Infrastructure;
using ProductService.Infrastructure.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services
    .AddAuthenticationAndAuthorization(builder.Configuration)
    .AddMassTransit(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    DbInitializer.Init(scope.ServiceProvider);
}

app.UseExceptionHandler("/error");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
