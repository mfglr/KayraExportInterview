using AuthService.Domain.Entities;
using Shared.Events;

namespace AuthService.Application.Commands.UpdateUserName
{
    internal class UpdateUserNameCommandMapper
    {
        public UserNameUpdatedEvent Map(User user) =>
            new(
                Guid.Parse(user.Id),
                user.CreatedAt,
                user.UpdatedAt,
                user.UserName!
            );
    }
}
