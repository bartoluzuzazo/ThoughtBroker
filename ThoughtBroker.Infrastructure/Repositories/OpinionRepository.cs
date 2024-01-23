using Microsoft.EntityFrameworkCore;
using ThoughtBroker.Domain.Opinions;
using ThoughtBroker.Infrastructure.Context;

namespace ThoughtBroker.Infrastructure.Repositories;

public class OpinionRepository : IOpinionRepository
{
    private readonly EfDbContext _context;
    
    public OpinionRepository(EfDbContext context)
    {
        _context = context;
    }
    
    public async Task CreateOpinion(Opinion opinion)
    {
        await _context.Opinions.AddAsync(opinion);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Opinion>> GetOpinionsByThoughtId(Guid thoughtId)
    {
        var opinions = await _context.Opinions.Where(o => o.ThoughtId == thoughtId).ToListAsync();
        return opinions;
    }
}