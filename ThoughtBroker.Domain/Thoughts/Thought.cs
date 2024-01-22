using ThoughtBroker.Domain.Base;
using ThoughtBroker.Domain.Opinions;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.Domain.Thoughts;

public partial class Thought : BaseEntity
{
    public string Content { get; set; } = null!;

    public Guid UserId { get; set; }

    public DateTime Timestamp { get; set; }

    public Guid? ParentId { get; set; }

    public virtual ICollection<Thought> InverseParent { get; set; } = new List<Thought>();

    public virtual ICollection<Opinion> Opinions { get; set; } = new List<Opinion>();

    public virtual Thought? Parent { get; set; }

    public virtual User User { get; set; } = null!;

    public static Thought Create(Guid authorId, string content, Guid? parentId)
    {
        var thought = new Thought()
        {
            Id = Guid.NewGuid(),
            Content = content,
            Timestamp = DateTime.Now,
            UserId = authorId
        };
        if (parentId is not null) thought.ParentId = parentId;
        return thought;
    }
}
