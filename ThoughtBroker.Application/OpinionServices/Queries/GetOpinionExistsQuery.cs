using MediatR;
using ThoughtBroker.Domain.Opinions;

namespace ThoughtBroker.Application.OpinionServices.Queries;

public record GetOpinionExistsQuery : IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid ThoughtId { get; set; }
}

public class GetOpinionExistsQueryHandler : IRequestHandler<GetOpinionExistsQuery, bool>
{
    private readonly IOpinionRepository _opinionRepository;
    
    public GetOpinionExistsQueryHandler(IOpinionRepository opinionRepository)
    {
        _opinionRepository = opinionRepository;
    }
    
    public Task<bool> Handle(GetOpinionExistsQuery request, CancellationToken cancellationToken)
    {
        return _opinionRepository.OpinionExists(request.UserId, request.ThoughtId);
    }
}