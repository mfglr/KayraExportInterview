using AuthService.Api.MassTransit;
using AuthService.Application;
using AuthService.Domain;
using AuthService.Infrastructure;
using AuthService.Infrastructure.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddMassTransit(builder.Configuration)
    .AddDomain()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    DbInitializer.Init(scope.ServiceProvider);
}

app.UseAuthorization();
app.MapControllers();
app.Run();
