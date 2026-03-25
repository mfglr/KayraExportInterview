using Shared.Exceptions;

namespace Application.Exceptions
{
    public class InsufficientPermissionToDeleteProductException() :
        ForbiddenException("You do not have permission to delete this product.");
}
