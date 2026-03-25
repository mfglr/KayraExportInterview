using Application.Exceptions;
using Domain;
using MediatR;

namespace Application.Queries.GetProductById
{
    internal class GetProductByIdQueryHandler(
        IProductRepository productRepository,
        IProductCacheService cacheService,
        ProductQueryResponseMapper mapper
    ) : IRequestHandler<GetProductByIdQueryRequest, ProductQueryResponse>
    {
        public async Task<ProductQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var dto = await cacheService.GetAsync(request.Id);
            if (dto != null) return dto;

            var product = 
                await productRepository.GetByIdAsync(request.Id, cancellationToken) ?? 
                throw new ProductNotFoundException();

            dto = mapper.Map(product);
            await cacheService.UpsertAsync(dto);
            
            return dto;
        }
    }
}
