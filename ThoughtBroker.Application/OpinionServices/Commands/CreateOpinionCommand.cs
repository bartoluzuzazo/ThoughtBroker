using MediatR;
using ThoughtBroker.Application.DTOs.OpinionDTOs.Create;
using ThoughtBroker.Domain.Opinions;

namespace ThoughtBroker.Application.OpinionServices.Commands;

public record CreateOpinionCommand : IRequest<OpinionCreateResponse>
{
    public Guid UserId { get; set; }
    public Guid ThoughtId { get; set; }
    public bool IsPositive { get; set; }
}

public class CreateOpinionCommandHandler : IRequestHandler<CreateOpinionCommand, OpinionCreateResponse>
{
    private readonly IOpinionRepository _opinionRepository;
    
    public CreateOpinionCommandHandler(IOpinionRepository opinionRepository)
    {
        _opinionRepository = opinionRepository;
    }
    
    public async Task<OpinionCreateResponse> Handle(CreateOpinionCommand request, CancellationToken cancellationToken)
    {
        var opinion = Opinion.Create(request.UserId, request.ThoughtId, request.IsPositive);
        await _opinionRepository.CreateOpinion(opinion);
        var response = new OpinionCreateResponse()
        {
            ThoughtId = opinion.ThoughtId,
            UserId = opinion.UserId
        };
        return response;
    }
}