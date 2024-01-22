using AutoMapper;
using MediatR;
using ThoughtBroker.Application.DTOs.UserDTOs.Read;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.Application.UserServices.Queries;

public record GetUserQuery : IRequest<UserGetResponse?>
{
    public Guid Id { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserGetResponse?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserGetResponse?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(request.Id);
        return user is not null ? _mapper.Map<UserGetResponse>(user) : null;
    }
}