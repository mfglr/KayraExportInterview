using Domain;
using MediatR;

namespace Application.Queries.SearchProduct
{
    internal class SearchProductQueryHandler(
        IProductRepository productRepository,
        SearchProductQueryMapper mapper
    ) : IRequestHandler<SearchProductQueryRequest, List<SearchProductQueryResponse>>
    {
        public async Task<List<SearchProductQueryResponse>> Handle(SearchProductQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await productRepository.SearchAsync(request.Key, request.Cursor, request.PageSize, cancellationToken);
            return [.. products.Select(mapper.Map)];
        }
    }
}
