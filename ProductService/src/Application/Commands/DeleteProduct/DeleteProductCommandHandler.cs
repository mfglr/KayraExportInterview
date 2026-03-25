using Application.Exceptions;
using Domain;
using MassTransit;
using MediatR;

namespace Application.Commands.DeleteProduct
{
    internal class DeleteProductCommandHandler(
        DeleteProductCommandMapper mapper,
        IProductRepository productRepository,
        IPublishEndpoint publishEndpoint,
        IProductCacheService cacheService,
        IAuthService authService
    ) : IRequestHandler<DeleteProductCommandRequest>
    {
        public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = 
                await productRepository.GetByIdAsync(request.Id, cancellationToken) ??
                throw new ProductNotFoundException();

            if (product.UserId != authService.UserId)
                throw new InsufficientPermissionToDeleteProductException();

            productRepository.Delete(product);

            var @event = mapper.Map(product);
            await publishEndpoint.Publish(@event, cancellationToken);

            await cacheService.DeleteAsync(product.Id);
        }
    }
}
