using AutoMapper;
using MediatR;
using ThoughtBroker.Application.DTOs.OpinionDTOs.Read;
using ThoughtBroker.Domain.Opinions;

namespace ThoughtBroker.Application.OpinionServices.Queries;

public record GetOpinionsQuery : IRequest<OpinionsGetResponse>
{
    public Guid ThoughtId { get; set; }
}

public class GetOpinionsQueryHandler : IRequestHandler<GetOpinionsQuery, OpinionsGetResponse>
{
    private readonly IOpinionRepository _opinionRepository;
    private readonly IMapper _mapper;
    
    public GetOpinionsQueryHandler(IOpinionRepository opinionRepository, IMapper mapper)
    {
        _opinionRepository = opinionRepository;
        _mapper = mapper;
    }
    
    public async Task<OpinionsGetResponse> Handle(GetOpinionsQuery request, CancellationToken cancellationToken)
    {
        var opinions = await _opinionRepository.GetOpinionsByThoughtId(request.ThoughtId);
        var response = new OpinionsGetResponse()
        {
            Opinions = opinions.Select(o => _mapper.Map<OpinionsGetResponseOpinion>(o)).ToList()
        };
        return response;
    }
}