using MediatR;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.Application.UserServices.Commands;

public record DeleteAccountCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
}

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public DeleteAccountCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Guid> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.DeleteAccountAsync(request.Id);
        return result;
    }
}