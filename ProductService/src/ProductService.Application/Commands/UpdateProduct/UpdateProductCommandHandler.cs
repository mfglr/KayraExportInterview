using MassTransit;
using MediatR;
using ProductService.Application.Exceptions;
using ProductService.Application.Queries;
using ProductService.Domain;

namespace ProductService.Application.Commands.UpdateProduct
{
    internal class UpdateProductCommandHandler(
        IProductRepository productRepository,
        UpdateProductCommandMapper mapper,
        IPublishEndpoint publishEndpoint,
        IProductCacheService cacheService,
        ProductQueryResponseMapper queryMapper
    ) : IRequestHandler<UpdateProductCommandRequest>
    {
        public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var title = new ProductTitle(request.Title);
            var description = new ProductDescription(request.Description);
            var currency = new Currency(request.Currency);
            var price = new ProductPrice(request.Price, currency);

            var product = 
                await productRepository.GetByIdAsync(request.Id, cancellationToken) ??
                throw new ProductNotFoundException();

            product.Update(title, description, price);

            var @event = mapper.Map(product);
            await publishEndpoint.Publish(@event, cancellationToken);

            await cacheService.UpsertAsync(queryMapper.Map(product));
        }
    }
}
