using Application.Commands.DeleteUserProducts;
using MassTransit;
using MediatR;
using Shared.Events;

namespace Product.Worker.MassTransit.Consumers.DeleteUserProducts_OnUserDeleted
{
    internal class DeleteUserProducts_OnUserDeleted_ProductService(IMediator mediator) : IConsumer<UserDeletedEvent>
    {
        public Task Consume(ConsumeContext<UserDeletedEvent> context) =>
            mediator.Send(new DeleteUserProductsCommandRequest(context.Message.Id), context.CancellationToken);
    }
}
