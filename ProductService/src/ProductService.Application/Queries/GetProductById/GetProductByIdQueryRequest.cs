using MediatR;

namespace ProductService.Application.Queries.GetProductById
{
    public record GetProductByIdQueryRequest(Guid Id) : IRequest<ProductQueryResponse>;
}
