namespace AuthService.Domain.Exceptions
{
    internal class InvalidPasswordException(string message) : Exception(message);
}