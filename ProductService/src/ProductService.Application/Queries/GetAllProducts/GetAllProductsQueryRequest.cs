using MediatR;

namespace ProductService.Application.Queries.GetAllProducts
{
    public record GetAllProductsQueryRequest(DateTime? Cursor, int PageSize = 20) : IRequest<List<ProductQueryResponse>>;
}
