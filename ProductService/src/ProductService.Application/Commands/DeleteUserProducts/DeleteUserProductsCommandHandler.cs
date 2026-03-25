using MassTransit;
using MediatR;
using ProductService.Domain;

namespace ProductService.Application.Commands.DeleteUserProducts
{
    internal class DeleteUserProductsCommandHandler(
        IProductRepository productRepository,
        DeleteUserProductsCommandMapper mapper,
        IPublishEndpoint publishEndpoint,
        IProductCacheService cacheService
    ) : IRequestHandler<DeleteUserProductsCommandRequest>
    {
        public async Task Handle(DeleteUserProductsCommandRequest request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            productRepository.Delete(products);

            var events = products.Select(mapper.Map);
            await publishEndpoint.PublishBatch(events, cancellationToken);

            await cacheService.DeleteAsync(products.Select(x => x.Id));
        }
    }
}
