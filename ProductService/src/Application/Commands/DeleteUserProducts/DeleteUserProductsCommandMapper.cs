using ProductService.Domain;
using Shared.Events;

namespace Application.Commands.DeleteUserProducts
{
    internal class DeleteUserProductsCommandMapper
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
