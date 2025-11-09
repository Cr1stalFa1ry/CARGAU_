namespace Domain.Entities;

public abstract class BaseEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
}

public abstract class BaseEntity<T> : BaseEntity
{
    public T Id { get; set; } = default!;
}