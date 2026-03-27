using MediatR;

namespace ProductService.Application.Queries.SearchProduct
{
    public record SearchProductQueryRequest(
        string? Key,
        DateTime? Cursor,
        int PageSize = 20
    ) : IRequest<List<ProductQueryResponse>>;
}
