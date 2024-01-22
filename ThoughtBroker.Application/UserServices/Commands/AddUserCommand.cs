using MediatR;
using Microsoft.AspNetCore.Identity;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.Application.UserServices.Commands;

public record AddUserCommand : IRequest<Guid>
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;

    public AddUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.UserExistsAsync(request.Username, request.Email)) return Guid.Empty;
        
        var passwordHasher = new PasswordHasher<User>();
        var hashedPassword = passwordHasher.HashPassword(new User(), request.Password);
        
        var user = User.Create(request.Username, request.Email, hashedPassword);

        await _userRepository.AddUserAsync(user);

        return user.Id;
    }
}