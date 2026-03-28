namespace AuthService.Application.Commands.CreateUser
{
    public record CreateUserCommandResponse(
        Guid Id,
        string UserName,
        TokenResponse Token
    );
}