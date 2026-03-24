using MediatR;

namespace Application.Queries.GetAllProducts
{
    public record GetAllProductsQueryRequest(DateTime? Cursor, int PageSize) : IRequest<List<ProductQueryResponse>>;
}
