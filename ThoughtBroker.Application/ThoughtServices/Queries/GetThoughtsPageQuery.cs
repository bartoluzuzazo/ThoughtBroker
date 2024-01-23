using AutoMapper;
using MediatR;
using ThoughtBroker.Application.DTOs.ThoughtDTOs.Read;
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
    private readonly IMapper _mapper;

    public GetThoughtsPageQueryHandler(IThoughtRepository thoughtRepository, IMapper mapper)
    {
        _thoughtRepository = thoughtRepository;
        _mapper = mapper;
    }
    
    public async Task<GetThoughtsResponse> Handle(GetThoughtsPageQuery request, CancellationToken cancellationToken)
    {
        var thoughts = await _thoughtRepository.GetThoughtsPageAsync(request.Page, request.Quantity);
        var count = await _thoughtRepository.CountRows();
        var response = new GetThoughtsResponse()
        {
            Thoughts = thoughts.Select(t =>
            {
                var tResponse = _mapper.Map<GetThoughtsResponseThought>(t);
                tResponse.Username = t.User.Username;
                return tResponse;
            }).ToList(),
            Pages = (count + request.Quantity - 1) / request.Quantity
        };
        return response;
    }
}