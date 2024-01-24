namespace ThoughtBroker.Domain.Opinions;

public interface IOpinionRepository
{
    public Task CreateOpinion(Opinion opinion);

    public Task<List<Opinion>> GetOpinionsByThoughtId(Guid thoughtId);

    public Task<bool> OpinionExists(Guid userId, Guid thoughtId);
}