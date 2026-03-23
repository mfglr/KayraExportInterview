namespace Shared
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public byte[]? RowVersion { get; private set; }

        public bool IsDeleted => DeletedAt != null;

        protected Entity()
        {
            Id = Guid.CreateVersion7();
            CreatedAt = DateTime.UtcNow;
        }
        protected void Update() => UpdatedAt = DateTime.UtcNow;
        public void Delete() => DeletedAt = DateTime.UtcNow;
    }
}
