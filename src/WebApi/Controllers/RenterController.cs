using Application.Common;
using Application.UseCases.Renter.CreateRenter.Inputs;
using Application.UseCases.Renter.UploadRenterLicenseImage.Inputs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class RenterController : ControllerBase
    {
        private readonly ILogger<RenterController> _logger;
        private readonly IMediator _mediator;

        public RenterController(ILogger<RenterController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Output), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRenterInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return new CreatedResult(string.Empty, result);

            return BadRequest(result);
        }

        [HttpPost("upload-license-image")]
        [ProducesResponseType(typeof(Output), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Output), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RequestRentalClosureAsync([FromBody] UploadRenterLicenseImageInput input, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(input, cancellationToken);

            if (result.IsValid)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
