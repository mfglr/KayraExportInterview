using ProductService.Domain;

namespace ProductService.Application.Queries
{
    internal class ProductQueryResponseMapper
    {
        public ProductQueryResponse_Price Map(ProductPrice price) =>
            new(
                price.Value,
                price.Currency.Value
            );

        public ProductQueryResponse Map(Product @event) =>
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
