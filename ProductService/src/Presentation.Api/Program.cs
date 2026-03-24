using Application;
using Infrastructure;
using Infrastructure.EfCore;
using Presentation.Api.Auth;
using Presentation.Api.MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
