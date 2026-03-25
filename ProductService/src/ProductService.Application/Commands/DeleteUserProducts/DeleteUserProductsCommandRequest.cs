using MediatR;

namespace ProductService.Application.Commands.DeleteUserProducts
{
    public record DeleteUserProductsCommandRequest(Guid UserId) : IRequest;
}
