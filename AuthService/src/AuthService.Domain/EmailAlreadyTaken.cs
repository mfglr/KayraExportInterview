namespace AuthService.Domain
{
    internal class EmailAlreadyTaken() : Exception("Email has been already taken!");
}