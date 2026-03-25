using Shared.Exceptions;

namespace ProductService.Application.Exceptions
{
    internal class ProductNotFoundException() : NotFoundException("Product not found!");
}