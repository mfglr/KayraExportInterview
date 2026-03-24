namespace Shared.Events
{
    public record ProductDeletedEvent_Price(
        decimal Price,
        string Currency
    );

    public record ProductDeletedEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid CategoryId,
        string Title,
        string Description,
        ProductDeletedEvent_Price Price
    );
}
