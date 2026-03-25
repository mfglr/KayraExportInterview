namespace Gateway.Api
{
    internal record RabbitMQOptions(
        string Host,
        string VirtualHost,
        string Password,
        string UserName,
        string LogExchangeName
    );
}
