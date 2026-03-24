using MediatR;

namespace Application.Queries.GetAllProducts
{
    public record GetAllProductsQueryRequest(Guid? Cursor, int PageSize) : IRequest<List<ProductQueryResponse>>;
}
