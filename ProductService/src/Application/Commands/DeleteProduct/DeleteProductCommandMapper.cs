using Domain;
using Shared.Events;

namespace Application.Commands.DeleteProduct
{
    internal class DeleteProductCommandMapper
    {
        public ProductDeletedEvent_Price Map(ProductPrice price) =>
            new(
                price.Value,
                price.Currency.Value
            );

        public ProductDeletedEvent Map(Product @event) =>
            new(
                @event.Id,
                @event.CreatedAt,
                @event.UpdatedAt,
                @event.CategoryId,
                @event.Title.Value,
                @event.Description.Value,
                Map(@event.Price)
            );
    }
}
