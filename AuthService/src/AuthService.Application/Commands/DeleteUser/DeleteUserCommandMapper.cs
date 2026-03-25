using AuthService.Domain.Entities;
using Shared.Events;

namespace AuthService.Application.Commands.DeleteUser
{
    internal class DeleteUserCommandMapper
    {
        public UserDeletedEvent Map(User user) =>
            new(Guid.Parse(user.Id));
    }
}
