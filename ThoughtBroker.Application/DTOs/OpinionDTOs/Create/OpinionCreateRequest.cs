namespace ThoughtBroker.Application.DTOs.OpinionDTOs.Create;

public class OpinionCreateRequest
{
    public Guid ThoughtId { get; set; }
    public bool IsPositive { get; set; }
}