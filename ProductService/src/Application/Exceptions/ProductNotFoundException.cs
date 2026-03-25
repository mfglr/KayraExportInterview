using Shared.Exceptions;

namespace Application.Exceptions
{
    internal class ProductNotFoundException() : NotFoundException("Product not found!");
}