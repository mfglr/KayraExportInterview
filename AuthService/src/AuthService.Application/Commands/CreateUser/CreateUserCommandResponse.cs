namespace AuthService.Application.Commands.CreateUser
{
    public record CreateUserCommandResponse(
        Guid Id,
        TokenResponse Token
    );
}