namespace AuthService.Domain
{
    internal class InvalidPassword(string message) : Exception(message);
}