namespace AuthService.Domain.Exceptions
{
    internal class EmailAlreadyTakenException() : Exception("Email has been already taken!");
}