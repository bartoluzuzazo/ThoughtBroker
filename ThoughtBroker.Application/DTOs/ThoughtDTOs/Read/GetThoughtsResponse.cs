namespace ThoughtBroker.Application.DTOs.ThoughtDTOs.Read;

public class GetThoughtsResponse
{
    public List<GetThoughtsResponseThought> Thoughts { get; set; }
    public int Pages { get; set; }
}

public class GetThoughtsResponseThought
{
    public string Content { get; set; } = null!;

    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public Guid? ParentId { get; set; }
}