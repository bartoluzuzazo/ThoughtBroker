using ThoughtBroker.Domain.Base;
using ThoughtBroker.Domain.Thoughts;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.Domain.Opinions;

public partial class Opinion : ValueObject
{
    public Guid UserId { get; set; }

    public Guid ThoughtId { get; set; }

    public bool IsPositive { get; set; }

    public virtual Thought Thought { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public static Opinion Create(Guid userId, Guid thoughtId, bool isPositive)
    {
        var opinion = new Opinion()
        {
            UserId = userId,
            ThoughtId = thoughtId,
            IsPositive = isPositive
        };
        return opinion;
    }
}
