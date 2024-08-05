using Application.UseCases.CreateMotorcycle.Inputs;
using Application.UseCases.CreateMotorcycle.Outputs;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("motorcycle")]
    public class MotorcycleController : ControllerBase
    {
        private readonly ILogger<MotorcycleController> _logger;
        private readonly IMediator _mediator;

        public MotorcycleController(ILogger<MotorcycleController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateMotorcycleOutput), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CreateMotorcycleOutput), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateMotorcycleInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
