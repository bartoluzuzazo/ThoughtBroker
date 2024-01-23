namespace ThoughtBroker.Domain.Opinions;

public interface IOpinionRepository
{
    public Task CreateOpinion(Opinion opinion);

    public Task<List<Opinion>> GetOpinionsByThoughtId(Guid thoughtId);
}