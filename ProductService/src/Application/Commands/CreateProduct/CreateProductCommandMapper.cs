using Domain;
using Shared.Events;

namespace Application.Commands.CreateProduct
{
    internal class CreateProductCommandMapper
    {
        public ProductCreatedEvent_Price Map(ProductPrice price) =>
            new(
                price.Value,
                price.Currency.Value
            );

        public ProductCreatedEvent Map(Product @event) =>
            new(
                @event.Id,
                @event.CreatedAt,
                @event.CategoryId,
                @event.Title.Value,
                @event.Description.Value,
                Map(@event.Price),
                @event.IsActive
            );
    }
}
