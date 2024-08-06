using Application.Common;
using Application.UseCases.CreateMotorcycle.Inputs;
using Application.UseCases.CreateMotorcycle.Outputs;
using Application.UseCases.DeleteMotorcycle.Inputs;
using Application.UseCases.ListMotorcycles.Inputs;
using Application.UseCases.ListMotorcycles.Outputs;
using Application.UseCases.ModifyMotorcyclePlate.Inputs;
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateMotorcycleInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return new CreatedResult(string.Empty, result);

            return BadRequest(result);
        }

        [HttpGet("list-motorcycles")]
        [ProducesResponseType(typeof(ListMotorcyclesOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ListMotorcyclesOutput), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListMotorcyclesAsync([FromQuery] ListMotorcyclesInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPatch("update-plate")]
        [ProducesResponseType(typeof(Output), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ModifyMotorcycleAsync([FromBody] ModifyMotorcyclePlateInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Output), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveMotorcycleAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var input = new DeleteMotorcycleInput { Id = id };
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
