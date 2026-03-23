using Domain;

namespace Application.Queries.SearchProduct
{
    internal class SearchProductQueryMapper
    {
        public SearchProductQueryResponse_Price Map(ProductPrice price) =>
            new(
                price.Value,
                price.Currency.Value
            );

        public SearchProductQueryResponse Map(Product @event) =>
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
