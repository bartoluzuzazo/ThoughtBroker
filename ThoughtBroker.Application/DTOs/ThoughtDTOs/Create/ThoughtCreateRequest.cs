namespace ThoughtBroker.Application.DTOs.ThoughtDTOs.Create;

public class ThoughtCreateRequest
{
    public string Content { get; set; } = null!;
    public Guid? ParentId { get; set; }
}