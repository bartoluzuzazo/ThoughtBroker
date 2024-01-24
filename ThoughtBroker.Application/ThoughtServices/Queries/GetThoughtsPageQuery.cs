using AutoMapper;
using MediatR;
using ThoughtBroker.Application.DTOs.ThoughtDTOs.Read;
using ThoughtBroker.Domain.Opinions;
using ThoughtBroker.Domain.Thoughts;

namespace ThoughtBroker.Application.ThoughtServices.Queries;

public record GetThoughtsPageQuery : IRequest<GetThoughtsResponse>
{
    public int Page { get; set; }
    public int Quantity { get; set; }
}

public class GetThoughtsPageQueryHandler : IRequestHandler<GetThoughtsPageQuery, GetThoughtsResponse>
{
    private readonly IThoughtRepository _thoughtRepository;
    private readonly IOpinionRepository _opinionRepository;
    private readonly IMapper _mapper;

    public GetThoughtsPageQueryHandler(IThoughtRepository thoughtRepository, IMapper mapper, IOpinionRepository opinionRepository)
    {
        _thoughtRepository = thoughtRepository;
        _mapper = mapper;
        _opinionRepository = opinionRepository;
    }
    
    public async Task<GetThoughtsResponse> Handle(GetThoughtsPageQuery request, CancellationToken cancellationToken)
    {
        var thoughts = await _thoughtRepository.GetThoughtsPageAsync(request.Page, request.Quantity);
        var count = await _thoughtRepository.CountRows();

        var thoughtsResponse = thoughts.Select(t =>
        {
            var opinions = _opinionRepository.GetOpinionsByThoughtId(t.Id).Result;
            var tResponse = _mapper.Map<GetThoughtsResponseThought>(t);
            tResponse.Username = t.User.Username;
            tResponse.Endorsements = opinions.Count(o => o.IsPositive);
            tResponse.Condemnations = opinions.Count(o => !o.IsPositive);
            return tResponse;
        }).ToList();
        
        var response = new GetThoughtsResponse()
        {
            Thoughts = thoughtsResponse,
            Pages = (count + request.Quantity - 1) / request.Quantity
        };
        return response;
    }
}