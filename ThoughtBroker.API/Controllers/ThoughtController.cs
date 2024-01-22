using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThoughtBroker.Application.DTOs.ThoughtDTOs.Create;
using ThoughtBroker.Application.ThoughtServices.Commands;
using ThoughtBroker.Application.ThoughtServices.Queries;

namespace ThoughtBroker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThoughtController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ThoughtController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> PostThought(ThoughtCreateRequest request)
        {
            var command = _mapper.Map<PostThoughtCommand>(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllThoughts()
        {
            var command = new GetAllThoughtsQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
