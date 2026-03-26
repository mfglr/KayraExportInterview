namespace LogService.Worker.RabbitMQ
{
    internal record RabbitMQOptions(
        string Host,
        string VirtualHost,
        string Password,
        string UserName,
        string LogExchangeName
    );
}
