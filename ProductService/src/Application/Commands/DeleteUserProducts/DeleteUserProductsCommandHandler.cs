using Domain;
using MassTransit;
using MediatR;

namespace Application.Commands.DeleteUserProducts
{
    internal class DeleteUserProductsCommandHandler(
        IProductRepository productRepository,
        DeleteUserProductsCommandMapper mapper,
        IPublishEndpoint publishEndpoint
    ) : IRequestHandler<DeleteUserProductsCommandRequest>
    {
        public async Task Handle(DeleteUserProductsCommandRequest request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            productRepository.Delete(products);

            var events = products.Select(mapper.Map);
            await publishEndpoint.PublishBatch(events, cancellationToken);
        }
    }
}
