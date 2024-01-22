using MediatR;
using ThoughtBroker.Domain.Thoughts;

namespace ThoughtBroker.Application.ThoughtServices.Queries;

public record GetAllThoughtsQuery : IRequest<List<Thought>>
{
}

public class GetAllThoughtsQueryHandler : IRequestHandler<GetAllThoughtsQuery, List<Thought>>
{
    private readonly IThoughtRepository _thoughtRepository;

    public GetAllThoughtsQueryHandler(IThoughtRepository thoughtRepository)
    {
        _thoughtRepository = thoughtRepository;
    }
    
    public async Task<List<Thought>> Handle(GetAllThoughtsQuery request, CancellationToken cancellationToken)
    {
        var thoughts = await _thoughtRepository.GetAllThoughtsAsync();
        return thoughts;
    }
}