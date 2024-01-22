using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ThoughtBroker.Application.DTOs.UserDTOs.Create;
using ThoughtBroker.Application.DTOs.UserDTOs.Login;
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
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        var command = _mapper.Map<UserLoginQuery>(request);
        var result = await _mediator.Send(command);
        if (result.Token.IsNullOrEmpty()) return Unauthorized();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(Guid request)
    {
        var command = new GetUserQuery{Id = request};
        var result = await _mediator.Send(command);
        return result is not null ? Ok(result) : NotFound();
    }
}