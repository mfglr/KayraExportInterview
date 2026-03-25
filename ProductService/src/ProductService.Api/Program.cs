using ProductService.Api.Auth;
using ProductService.Api.MassTransit;
using ProductService.Api.Middlewares;
using ProductService.Api.SerilogRegistration;
using ProductService.Application;
using ProductService.Infrastructure;
using ProductService.Infrastructure.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddCustomSerilog();
builder.Services.AddControllers();
builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
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
