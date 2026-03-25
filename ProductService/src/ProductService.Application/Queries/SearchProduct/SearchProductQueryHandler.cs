using MediatR;
using ProductService.Domain;

namespace ProductService.Application.Queries.SearchProduct
{
    internal class SearchProductQueryHandler(
        IProductRepository productRepository,
        ProductQueryResponseMapper mapper
    ) : IRequestHandler<SearchProductQueryRequest, List<ProductQueryResponse>>
    {
        public async Task<List<ProductQueryResponse>> Handle(SearchProductQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await productRepository.SearchAsync(request.Key, request.Cursor, request.PageSize, cancellationToken);
            return [.. products.Select(mapper.Map)];
        }
    }
}
