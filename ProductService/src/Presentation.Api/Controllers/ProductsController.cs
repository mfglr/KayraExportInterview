using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Commands.CreateProduct;
using ProductService.Application.Commands.DeleteProduct;
using ProductService.Application.Commands.UpdateProduct;
using ProductService.Application.Queries;
using ProductService.Application.Queries.GetAllProducts;
using ProductService.Application.Queries.GetProductById;
using ProductService.Application.Queries.SearchProduct;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id:guid}")]
        public Task<ProductQueryResponse> GetById(Guid id, CancellationToken cancellationToken) =>
            mediator.Send(new GetProductByIdQueryRequest(id), cancellationToken);

        [HttpGet]
        public Task<List<ProductQueryResponse>> Search([FromQuery] SearchProductQueryRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);

        [HttpGet]
        public Task<List<ProductQueryResponse>> GetAll([FromQuery] GetAllProductsQueryRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);

        [Authorize("user")]
        [HttpPost]
        public Task<CreateProductCommandResponse> Create(CreateProductCommandRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);

        [Authorize("user")]
        [HttpPut]
        public Task Update(UpdateProductCommandRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);

        [Authorize("user")]
        [HttpDelete("{id:guid}")]
        public Task Delete(Guid id, CancellationToken cancellationToken) =>
            mediator.Send(new DeleteProductCommandRequest(id), cancellationToken);
    }
}
