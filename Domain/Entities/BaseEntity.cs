namespace Domain.Entities;

public abstract class BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public abstract class BaseEntity<T> : BaseEntity
{
    public T Id { get; set; } = default!;
}