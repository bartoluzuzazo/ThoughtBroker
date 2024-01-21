using MediatR;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.Application.UserServices.Queries;

public record GetUserQuery : IRequest<User?>
{
    public Guid Id { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(request.Id);
        return user;
    }
}