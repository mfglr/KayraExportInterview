using AuthService.Domain;
using Shared.Events;

namespace AuthService.Application.Commands.CreateUser
{
    internal class CreateUserCommandMapper
    {
        public UserCreatedEvent Map(User user) =>
            new(
                Guid.Parse(user.Id),
                user.CreatedAt,
                user.UpdatedAt,
                user.UserName!
            );
    }
}
