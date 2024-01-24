namespace ThoughtBroker.Application.DTOs.ThoughtDTOs.Read;

public class GetThoughtsResponse
{
    public List<GetThoughtsResponseThought> Thoughts { get; set; }
    public int Pages { get; set; }
}

public class GetThoughtsResponseThought
{
    public Guid Id { get; set; }
    
    public string Content { get; set; } = null!;

    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public Guid? ParentId { get; set; }

    public int Endorsements { get; set; }
    
    public int Condemnations { get; set; }
}