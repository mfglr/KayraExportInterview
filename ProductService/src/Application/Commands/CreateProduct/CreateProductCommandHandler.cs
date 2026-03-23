using Domain;
using MassTransit;
using MediatR;

namespace Application.Commands.CreateProduct
{
    internal class CreateProductCommandHandler(
        IProductRepository repository,
        IPublishEndpoint publishEndpoint,
        CreateProductCommandMapper mapper
    ) : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var title = new ProductTitle(request.Title);
            var description = new ProductDescription(request.Description);
            var currency = new Currency(request.Currency);
            var price = new ProductPrice(request.Price, currency);
            var product = new Product(request.CategoryId, title, description, price);

            await repository.CreateAsync(product, cancellationToken);

            var @event = mapper.Map(product);
            await publishEndpoint.Publish(@event, cancellationToken);

            return new(product.Id);
        }
    }
}
