using ThoughtBroker.Domain.Base;
using ThoughtBroker.Domain.Opinions;
using ThoughtBroker.Domain.Thoughts;

namespace ThoughtBroker.Domain.Users;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<Opinion> Opinions { get; set; } = new List<Opinion>();

    public virtual ICollection<Thought> Thoughts { get; set; } = new List<Thought>();

    public virtual ICollection<User> User2s { get; set; } = new List<User>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public static User Create(string username, string email, string password)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            PasswordHash = password //TODO: add encryption
        };
        return user;
    }
}