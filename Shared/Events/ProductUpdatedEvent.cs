namespace Shared.Events
{
    public record ProductUpdatedEvent_Price(
        decimal Price,
        string Currency
    );

    public record ProductUpdatedEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid CategoryId,
        string Title,
        string Description,
        ProductUpdatedEvent_Price Price,
        bool IsActive
    );
}
