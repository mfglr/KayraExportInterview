using MediatR;

namespace Application.Commands.DeleteProduct
{
    public record DeleteProductCommandRequest(Guid Id) : IRequest;
}
