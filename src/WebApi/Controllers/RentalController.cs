using Application.Common;
using Application.UseCases.Rental.RequestMotorcycleRental.Inputs;
using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Inputs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("rental")]
    public class RentalController : ControllerBase
    {
        private readonly ILogger<RentalController> _logger;
        private readonly IMediator _mediator;

        public RentalController(ILogger<RentalController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("request-rental")]
        [ProducesResponseType(typeof(Output), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RequestRentalAsync([FromBody] RequestMotorcycleRentalInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("request-rental-closure")]
        [ProducesResponseType(typeof(Output), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RequestRentalClosureAsync([FromBody] RequestMotorcycleRentalClosureInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
