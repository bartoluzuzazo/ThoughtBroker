using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThoughtBroker.API.DTOs.UserDTOs.Create;
using ThoughtBroker.API.DTOs.UserDTOs.Get;
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

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserCreateRequest request)
    {
        var command = _mapper.Map<AddUserCommand>(request);

        var result = await _mediator.Send(command);

        var response = new UserCreateResponse()
        {
            Id = result
        };
        
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetUser(Guid request)
    {
        var command = new GetUserQuery{Id = request};
        var result = await _mediator.Send(command);
        if (result is null) return NotFound();
        var response = _mapper.Map<UserGetResponse>(result);
        return Ok(response);
    }
}