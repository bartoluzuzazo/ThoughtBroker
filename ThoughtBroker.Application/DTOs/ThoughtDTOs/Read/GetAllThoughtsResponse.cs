using ThoughtBroker.Domain.Thoughts;

namespace ThoughtBroker.API.DTOs.ThoughtDTOs.Read;

public class GetAllThoughtsResponse
{
    public List<GetAllThoughtsResponseThought> Thoughts { get; set; }
}

public class GetAllThoughtsResponseThought
{
    public string Content { get; set; } = null!;

    public Guid UserId { get; set; }
    
    public string Username { get; set; }

    public DateTime Timestamp { get; set; }

    public Guid? ParentId { get; set; }
}