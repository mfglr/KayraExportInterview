using MediatR;

namespace Application.Queries.SearchProduct
{
    public record SearchProductQueryRequest(
        string Key,
        DateTime? Cursor,
        int PageSize
    ) : IRequest<List<ProductQueryResponse>>;
}
