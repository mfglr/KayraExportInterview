using Domain;
using MediatR;

namespace Application.Queries.GetAllProducts
{
    internal class GetAllProductsQueryHandler(
        IProductRepository repository,
        ProductQueryResponseMapper mapper,
        IProductListCacheService cacheService
    ) : IRequestHandler<GetAllProductsQueryRequest, List<ProductQueryResponse>>
    {
        public async Task<List<ProductQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheId = cacheService.Id(request.Cursor, request.PageSize);
            var dtos = await cacheService.GetAsync(cacheId);
            if (dtos != null) return dtos;

            var products = await repository.GetAllAsync(request.Cursor, request.PageSize, cancellationToken);
            dtos = [..products.Select(mapper.Map)];
            await cacheService.CreateAsync(cacheId, dtos);
            return dtos;
        }
    }
}
