namespace ThoughtBroker.Domain.Thoughts;

public interface IThoughtRepository
{
    public Task CreateThoughtAsync(Thought thought);

    public Task<List<Thought>> GetAllThoughtsAsync();

    public Task<List<Thought>> GetThoughtsPageAsync(int page, int quantity);

    public Task<int> CountRows();
}