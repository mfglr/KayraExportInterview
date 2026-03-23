namespace Application.Queries.SearchProduct
{
    public record SearchProductQueryResponse_Price(
        decimal Price,
        string Currency
    );

    public record SearchProductQueryResponse(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        Guid CategoryId,
        string Title,
        string Description,
        SearchProductQueryResponse_Price Price
    );
}