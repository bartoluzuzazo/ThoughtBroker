using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThoughtBroker.Application.DTOs.OpinionDTOs.Create;
using ThoughtBroker.Application.OpinionServices.Commands;
using ThoughtBroker.Application.OpinionServices.Queries;

namespace ThoughtBroker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpinionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public OpinionController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOpinion(OpinionCreateRequest request)
        {
            var command = _mapper.Map<CreateOpinionCommand>(request);
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            command.UserId = Guid.Parse(identity!.Claims.First(c => c.Type == "UserId").Value);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOpinions), result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOpinions(Guid thoughtId)
        {
            var command = new GetOpinionsQuery()
            {
                ThoughtId = thoughtId
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        
        [HttpGet("exists")]
        public async Task<IActionResult> GetOpinionExists(Guid thoughtId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity is null) return Ok(true);
            var login = identity.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (login is null) return Ok(true);
            var command = new GetOpinionExistsQuery()
            {
                UserId = Guid.Parse(login.Value),
                ThoughtId = thoughtId
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
