namespace Shared.Events
{
    public record UserCreatedEvent(
        Guid Id,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        string UserName
    );
}
