using LogService.Application;
using LogService.Infractructure;
using LogService.Worker.RabbitMQ;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddRabbitMQ(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrstructure(builder.Configuration);

var host = builder.Build();
host.Run();
