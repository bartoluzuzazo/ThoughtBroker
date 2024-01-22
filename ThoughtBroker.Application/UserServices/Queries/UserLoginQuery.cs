using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ThoughtBroker.API.DTOs.UserDTOs.Login;
using ThoughtBroker.Domain.Users;

namespace ThoughtBroker.Application.UserServices.Queries;

public record UserLoginQuery : IRequest<UserLoginResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, UserLoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    
    public UserLoginQueryHandler(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }
    
    public async Task<UserLoginResponse> Handle(UserLoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserAsync(request.Email);
        if (user is null) return new UserLoginResponse() { Token = string.Empty };
        var passwordHasher = new PasswordHasher<User>();
        var isVerified = passwordHasher.VerifyHashedPassword(new User(), user.PasswordHash, request.Password) == PasswordVerificationResult.Success;
        if (!isVerified) return new UserLoginResponse() { Token = string.Empty };
        
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>()
        {
            new ("UserId", user.Id.ToString()),
            new ("Username", user.Username),
            new ("Role", "user")
        };
        
        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
            Expires = DateTime.UtcNow.AddMinutes(59),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!)),
                SecurityAlgorithms.HmacSha256
            )
            
        };
        var token = tokenHandler.CreateToken(tokenDescription);

        return new UserLoginResponse()
        {
            Token = tokenHandler.WriteToken(token)
        };
    }
}