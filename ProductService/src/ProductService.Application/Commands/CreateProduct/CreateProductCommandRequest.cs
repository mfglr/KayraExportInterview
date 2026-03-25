using MediatR;

namespace ProductService.Application.Commands.CreateProduct
{
    public record CreateProductCommandRequest(
        Guid CategoryId,
        string Title,
        string Description,
        decimal Price,
        string Currency
    ) : IRequest<CreateProductCommandResponse>;
}
