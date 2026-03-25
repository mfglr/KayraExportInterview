using MediatR;

namespace ProductService.Application.Commands.UpdateProduct
{
    public record UpdateProductCommandRequest(
        Guid Id,
        string Title,
        string Description,
        decimal Price,
        string Currency
    ) : IRequest;
}
