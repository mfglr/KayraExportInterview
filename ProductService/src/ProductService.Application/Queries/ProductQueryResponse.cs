namespace ProductService.Application.Queries
{
    public record ProductQueryResponse_Price(
        decimal Price,
        string Currency
    );

    public record ProductQueryResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid CategoryId,
        string Title,
        string Description,
        ProductQueryResponse_Price Price
    );
}