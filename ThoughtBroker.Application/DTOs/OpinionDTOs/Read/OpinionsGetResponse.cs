namespace ThoughtBroker.Application.DTOs.OpinionDTOs.Read;

public class OpinionsGetResponse
{
    public List<OpinionsGetResponseOpinion> Opinions { get; set; }
}

public class OpinionsGetResponseOpinion
{
    public Guid UserId { get; set; }
    public Guid ThoughtId { get; set; }
    public bool IsPositive { get; set; }
}