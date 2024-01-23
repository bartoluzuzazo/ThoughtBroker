using AutoMapper;
using MediatR;
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
    }
}
