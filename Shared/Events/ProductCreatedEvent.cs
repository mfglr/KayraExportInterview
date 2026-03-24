namespace Shared.Events
{
    public record ProductCreatedEvent_Price(
        decimal Price,
        string Currency
    );

    public record ProductCreatedEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid CategoryId,
        string Title,
        string Description,
        ProductCreatedEvent_Price Price,
        bool IsActive
    );
}
