using ProductService.Domain;
using MediatR;

namespace Application.Queries.GetAllProducts
{
    internal class GetAllProductsQueryHandler(
        IProductRepository repository,
        ProductQueryResponseMapper mapper,
        IProductCacheService cacheService
    ) : IRequestHandler<GetAllProductsQueryRequest, List<ProductQueryResponse>>
    {
        public async Task<List<ProductQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var dtos = await cacheService.GetAsync(request.PageSize, request.Cursor);
            if (dtos != null) return dtos;

            var products = await repository.GetAllAsync(request.Cursor, request.PageSize, cancellationToken);
            dtos = [..products.Select(mapper.Map)];
            await cacheService.UpsertAsync(request.PageSize, request.Cursor, dtos);
            return dtos;
        }
    }
}
