namespace Shared
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public byte[]? RowVersion { get; private set; }

        protected Entity()
        {
            Id = Guid.CreateVersion7();
            CreatedAt = DateTime.UtcNow;
        }
        protected void Update() => UpdatedAt = DateTime.UtcNow;
    }
}
