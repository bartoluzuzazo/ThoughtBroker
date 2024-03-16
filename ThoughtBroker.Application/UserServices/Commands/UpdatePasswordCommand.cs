using MediatR;
using Microsoft.AspNetCore.Identity;
using ThoughtBroker.Application.DTOs.UserDTOs.Update;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.Application.UserServices.Commands;

public record UpdatePasswordCommand : IRequest<UserPutPasswordResponse>
{
    public Guid Id { get; set; }
    public string Password { get; set; }
}

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UserPutPasswordResponse>
{
    private readonly IUserRepository _userRepository;

    public UpdatePasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserPutPasswordResponse> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var passwordHasher = new PasswordHasher<User>();
        var passwordHash = passwordHasher.HashPassword(new User(), request.Password);
        var result = await _userRepository.PutPasswordAsync(request.Id, passwordHash);
        var response = new UserPutPasswordResponse()
        {
            Id = result
        };
        return response;
    }
}