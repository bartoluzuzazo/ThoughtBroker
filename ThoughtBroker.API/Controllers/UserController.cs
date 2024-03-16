using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ThoughtBroker.Application.DTOs.UserDTOs.Create;
using ThoughtBroker.Application.DTOs.UserDTOs.Login;
using ThoughtBroker.Application.DTOs.UserDTOs.Read;
using ThoughtBroker.Application.DTOs.UserDTOs.Update;
using ThoughtBroker.Application.UserServices.Commands;
using ThoughtBroker.Application.UserServices.Queries;

namespace ThoughtBroker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser(UserCreateRequest request)
    {
        var command = _mapper.Map<AddUserCommand>(request);
        var result = await _mediator.Send(command);
        if (result.Id.Equals(Guid.Empty)) return Conflict();
        return CreatedAtAction(nameof(Login), result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        var command = _mapper.Map<UserLoginQuery>(request);
        var result = await _mediator.Send(command);
        if (result.Token.IsNullOrEmpty()) return Unauthorized();
        return Ok(result);
    }

    [Authorize]
    [HttpPut("password")]
    public async Task<IActionResult> ChangePassword(UserPutPasswordRequest request)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = Guid.Parse(identity!.Claims.First(c => c.Type == "UserId").Value);
        var command = new UpdatePasswordCommand()
        {
            Id = id,
            Password = request.Password
        };
        var result = await _mediator.Send(command);
        if (result.Id == Guid.Empty) return NotFound();
        return Ok(result);
    }
    
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteAccount()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = Guid.Parse(identity!.Claims.First(c => c.Type == "UserId").Value);
        var command = new DeleteAccountCommand()
        {
            Id = id
        };
        var result = await _mediator.Send(command);
        if (result == Guid.Empty) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(Guid request)
    {
        var command = new GetUserQuery{Id = request};
        var result = await _mediator.Send(command);
        return result is not null ? Ok(result) : NotFound();
    }
    
    [HttpGet("claims")]
    [Authorize]
    public IActionResult GetClaims()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var response = new UserGetClaimsResponse()
        {
            Id = identity!.Claims.First(c => c.Type == "UserId").Value,
            Username = identity!.Claims.First(c => c.Type == "Username").Value,
            Role = identity!.Claims.First(c => c.Type == "Role").Value
        };
        return Ok(response);
    }
}