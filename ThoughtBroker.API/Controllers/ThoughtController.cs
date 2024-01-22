using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThoughtBroker.API.DTOs.ThoughtDTOs.Create;
using ThoughtBroker.API.DTOs.ThoughtDTOs.Read;
using ThoughtBroker.Application.ThoughtServices.Commands;
using ThoughtBroker.Application.ThoughtServices.Queries;
using ThoughtBroker.Domain.Thoughts;

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

            var response = new ThoughtCreateResponse()
            {
                Id = result
            };
        
            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllThoughts()
        {
            var command = new GetAllThoughtsQuery();
            var result = await _mediator.Send(command);
            var response = new GetAllThoughtsResponse()
            {
                Thoughts = result.Select(t =>
                {
                    var t_response = _mapper.Map<GetAllThoughtsResponseThought>(t);
                    t_response.Username = t.User.Username;
                    return t_response;
                }).ToList()
            };
            return Ok(response);
        }
    }
}
