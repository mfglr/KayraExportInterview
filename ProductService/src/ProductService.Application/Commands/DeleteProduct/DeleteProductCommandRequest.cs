using MediatR;

namespace ProductService.Application.Commands.DeleteProduct
{
    public record DeleteProductCommandRequest(Guid Id) : IRequest;
}
