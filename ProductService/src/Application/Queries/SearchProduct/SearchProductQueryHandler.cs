using Domain;
using MediatR;

namespace Application.Queries.SearchProduct
{
    internal class SearchProductQueryHandler(
        IProductRepository productRepository,
        ProductQueryResponseMapper mapper,
        IProductSerchCacheService cachService
    ) : IRequestHandler<SearchProductQueryRequest, List<ProductQueryResponse>>
    {
        public async Task<List<ProductQueryResponse>> Handle(SearchProductQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheId = cachService.Id(request.Key, request.Cursor, request.PageSize);
            var dtos = await cachService.GetAsync(cacheId);
            if (dtos != null) return dtos;

            var products = await productRepository.SearchAsync(request.Key, request.Cursor, request.PageSize, cancellationToken);
            dtos = [.. products.Select(mapper.Map)];
            await cachService.CreateAsync(cacheId, dtos);
            
            return dtos;
        }
    }
}
