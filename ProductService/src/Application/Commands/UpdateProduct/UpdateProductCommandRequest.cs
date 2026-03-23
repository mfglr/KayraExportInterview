using MediatR;

namespace Application.Commands.UpdateProduct
{
    public record UpdateProductCommandRequest(
        Guid Id,
        string Title,
        string Description,
        decimal Price,
        string Currency
    ) : IRequest;
}
