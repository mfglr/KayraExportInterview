using Application.Commands.CreateProduct;
using Application.Commands.UpdateProduct;
using Application.Queries;
using Application.Queries.GetAllProducts;
using Application.Queries.GetProductById;
using Application.Queries.SearchProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public Task<CreateProductCommandResponse> Create(CreateProductCommandRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);

        [HttpPut]
        public Task Update(UpdateProductCommandRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);
    }
}
