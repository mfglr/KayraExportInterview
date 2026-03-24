using MediatR;

namespace Application.Queries.SearchProduct
{
    public record SearchProductQueryRequest(
        string Key,
        Guid? Cursor,
        int PageSize
    ) : IRequest<List<ProductQueryResponse>>;
}
