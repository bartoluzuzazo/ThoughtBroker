using MediatR;
using Microsoft.AspNetCore.Identity;
using ThoughtBroker.Application.DTOs.UserDTOs.Create;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.Application.UserServices.Commands;

public record AddUserCommand : IRequest<UserCreateResponse>
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserCreateResponse>
{
    private readonly IUserRepository _userRepository;

    public AddUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<UserCreateResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var response = new UserCreateResponse()
        {
            Id = Guid.Empty
        };
        
        if (await _userRepository.UserExistsAsync(request.Username, request.Email)) return response;
        
        var passwordHasher = new PasswordHasher<User>();
        var hashedPassword = passwordHasher.HashPassword(new User(), request.Password);
        
        var user = User.Create(request.Username, request.Email, hashedPassword);

        await _userRepository.AddUserAsync(user);

        response.Id = user.Id;
        
        return response;
    }
}