namespace Shared.Exceptions
{
    public class NotFoundException(string message) : AppException(message);
}
