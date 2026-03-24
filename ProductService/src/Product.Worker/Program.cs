using Application;
using Infrastructure;
using Product.Worker.Auth;
using Product.Worker.MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services
    .AddAuth()
    .AddMassTransit(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();
