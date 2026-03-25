namespace ProductService.Api.MassTransit
{
    internal record MassTransitOptions(
        string Host,
        string VirtualHost,
        string Password,
        string UserName
    );
}
