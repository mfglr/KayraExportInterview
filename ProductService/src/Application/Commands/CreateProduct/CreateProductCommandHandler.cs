using Application.Queries;
using ProductService.Domain;
using MassTransit;
using MediatR;

namespace Application.Commands.CreateProduct
{
    internal class CreateProductCommandHandler(
        IProductRepository repository,
        IPublishEndpoint publishEndpoint,
        CreateProductCommandMapper mapper,
        ProductQueryResponseMapper queryMapper,
        IProductCacheService cacheService,
        IAuthService authService
    ) : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = authService.UserId;
            var title = new ProductTitle(request.Title);
            var description = new ProductDescription(request.Description);
            var currency = new Currency(request.Currency);
            var price = new ProductPrice(request.Price, currency);
            var product = new Product(userId, request.CategoryId, title, description, price);

            await repository.CreateAsync(product, cancellationToken);

            var @event = mapper.Map(product);
            await publishEndpoint.Publish(@event, cancellationToken);

            await cacheService.UpsertAsync(queryMapper.Map(product));

            return new(product.Id);
        }
    }
}
