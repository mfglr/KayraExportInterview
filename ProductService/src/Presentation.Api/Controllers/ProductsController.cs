using Application.Commands.CreateProduct;
using Application.Commands.UpdateProduct;
using Application.Queries.SearchProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public Task<List<SearchProductQueryResponse>> Search([FromQuery] SearchProductQueryRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);

        [HttpPost]
        public Task<CreateProductCommandResponse> Create(CreateProductCommandRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);

        [HttpPut]
        public Task Update(UpdateProductCommandRequest request, CancellationToken cancellationToken) =>
            mediator.Send(request, cancellationToken);
    }
}
