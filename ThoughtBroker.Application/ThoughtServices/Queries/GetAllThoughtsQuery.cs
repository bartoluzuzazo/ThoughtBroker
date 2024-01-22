using AutoMapper;
using MediatR;
using ThoughtBroker.Application.DTOs.ThoughtDTOs.Read;
using ThoughtBroker.Domain.Thoughts;

namespace ThoughtBroker.Application.ThoughtServices.Queries;

public record GetAllThoughtsQuery : IRequest<GetAllThoughtsResponse>
{
}

public class GetAllThoughtsQueryHandler : IRequestHandler<GetAllThoughtsQuery, GetAllThoughtsResponse>
{
    private readonly IThoughtRepository _thoughtRepository;
    private readonly IMapper _mapper;

    public GetAllThoughtsQueryHandler(IThoughtRepository thoughtRepository, IMapper mapper)
    {
        _thoughtRepository = thoughtRepository;
        _mapper = mapper;
    }
    
    public async Task<GetAllThoughtsResponse> Handle(GetAllThoughtsQuery request, CancellationToken cancellationToken)
    {
        var thoughts = await _thoughtRepository.GetAllThoughtsAsync();
        var response = new GetAllThoughtsResponse()
        {
            Thoughts = thoughts.Select(t =>
            {
                var tResponse = _mapper.Map<GetAllThoughtsResponseThought>(t);
                tResponse.Username = t.User.Username;
                return tResponse;
            }).ToList()
        };
        return response;
    }
}