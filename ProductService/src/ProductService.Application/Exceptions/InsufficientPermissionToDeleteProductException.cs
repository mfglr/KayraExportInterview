using Shared.Exceptions;

namespace ProductService.Application.Exceptions
{
    public class InsufficientPermissionToDeleteProductException() :
        ForbiddenException("You do not have permission to delete this product.");
}
