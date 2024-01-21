using System.ComponentModel.DataAnnotations;

namespace ThoughtBroker.Domain.Base;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; init; }

    private bool Equals(BaseEntity entity)
    {
        return Id.Equals(entity.Id);
    }

    public static bool operator ==(BaseEntity a, BaseEntity b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(BaseEntity a, BaseEntity b)
    {
        return !(a == b);
    }
}