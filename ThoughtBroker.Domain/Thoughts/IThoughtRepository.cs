namespace ThoughtBroker.Domain.Thoughts;

public interface IThoughtRepository
{
    public Task CreateThoughtAsync(Thought thought);

    public Task<List<Thought>> GetAllThoughtsAsync();
}