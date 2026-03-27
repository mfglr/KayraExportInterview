namespace Shared.Events
{
    public record UserNameUpdatedEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        string UserName
    );
}
