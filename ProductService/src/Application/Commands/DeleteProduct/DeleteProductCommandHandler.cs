using Domain;
using MassTransit;
using MediatR;

namespace Application.Commands.DeleteProduct
{
    internal class DeleteProductCommandHandler(
        DeleteProductCommandMapper mapper,
        IProductRepository productRepository,
        IPublishEndpoint publishEndpoint
    ) : IRequestHandler<DeleteProductCommandRequest>
    {
        public async Task Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = 
                await productRepository.GetByIdAsync(request.Id, cancellationToken) ??
                throw new ProductNotFoundException();

            productRepository.Delete(product);

            var @event = mapper.Map(product);
            await publishEndpoint.Publish(@event, cancellationToken);
        }
    }
}
