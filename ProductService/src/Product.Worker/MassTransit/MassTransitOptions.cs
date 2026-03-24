namespace Product.Worker.MassTransit
{
    internal record MassTransitOptions(
        string Host,
        string VirtualHost,
        string Password,
        string UserName
    );
}
