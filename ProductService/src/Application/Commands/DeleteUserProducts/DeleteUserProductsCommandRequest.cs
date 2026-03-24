using MediatR;

namespace Application.Commands.DeleteUserProducts
{
    public record DeleteUserProductsCommandRequest(Guid UserId) : IRequest;
}
