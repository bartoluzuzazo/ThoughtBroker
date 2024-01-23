using Microsoft.EntityFrameworkCore;
using ThoughtBroker.Domain.Thoughts;
using ThoughtBroker.Infrastructure.Context;

namespace ThoughtBroker.Infrastructure.Repositories;

public class ThoughtRepository : IThoughtRepository
{
    private readonly EfDbContext _context;

    public ThoughtRepository(EfDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateThoughtAsync(Thought thought)
    {
        await _context.Thoughts.AddAsync(thought);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Thought>> GetAllThoughtsAsync()
    {
        var thoughts = await _context.Thoughts.Include(t => t.User).OrderByDescending(t => t.Timestamp).ToListAsync();
        return thoughts;
    }

    public async Task<List<Thought>> GetThoughtsPageAsync(int page, int quantity)
    {
        var thoughts = await _context.Thoughts.Include(t => t.User).OrderByDescending(t => t.Timestamp)
            .Skip((page - 1) * quantity).Take(quantity).ToListAsync();
        return thoughts;
    }

    public async Task<int> CountRows()
    {
        var count = await _context.Thoughts.CountAsync();
        return count;
    }
}