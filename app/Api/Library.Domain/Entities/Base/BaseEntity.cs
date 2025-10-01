namespace Library.Domain.Entities.Base;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public bool Active { get; protected set; }
    public DateTime CreatedAt { get; protected set; }

    public BaseEntity()
    {
        Active = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void ChangeActive(bool active = true)
        => Active = active;
}