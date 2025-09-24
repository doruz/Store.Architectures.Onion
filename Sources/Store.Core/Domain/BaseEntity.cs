namespace Store.Core.Domain
{
    public abstract class BaseEntity
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}