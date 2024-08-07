using Application.Common;
using Application.UseCases.Rental.RequestMotorcycleRental.Inputs;
using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Inputs;
using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Outputs;
using Application.UseCases.Renter.CreateRenter.Inputs;
using Application.UseCases.Renter.UploadRenterLicenseImage.Inputs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Controllers;

namespace UnitTests.WebApi.Controllers
{
    public class RentalControllerTests
    {
        private readonly IFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly ILogger<RentalController> _logger;
        private readonly IMediator _mediator;

        private readonly RentalController _controller;

        public RentalControllerTests()
        {
            _fixture = new Fixture();
            _cancellationToken = new CancellationToken();

            _logger = Substitute.For<ILogger<RentalController>>();
            _mediator = Substitute.For<IMediator>();
            _controller = new(_logger, _mediator);
        }

        [Fact(DisplayName = nameof(ShouldRequestRentalAsyncReturnOk))]
        [Trait("WebApi", "RentalControllerTests")]
        public async Task ShouldRequestRentalAsyncReturnOk()
        {
            //arrange
            var input = _fixture.Create<RequestMotorcycleRentalInput>();
            var output = new Output();

            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _controller.RequestRentalAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var resultOutput = (ObjectResult)result;
            resultOutput.Should().NotBeNull();
            resultOutput.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultOutput.Value.Should().BeOfType<Output>();

            var outputValue = (Output)resultOutput.Value!;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeTrue();
            outputValue.ErrorMessages.Should().BeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldRequestRentalAsyncReturnBadRequestWhenResultIsInvalid))]
        [Trait("WebApi", "RentalControllerTests")]
        public async Task ShouldRequestRentalAsyncReturnBadRequestWhenResultIsInvalid()
        {
            //arrange
            var input = _fixture.Create<RequestMotorcycleRentalInput>();

            var output = new Output();
            output.ErrorMessages.Add("fail");

            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _controller.RequestRentalAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var badRequestOutput = (ObjectResult)result;
            badRequestOutput.Should().NotBeNull();
            badRequestOutput.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestOutput.Value.Should().BeOfType<Output>();

            var outputValue = (Output)badRequestOutput.Value!;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeFalse();
            outputValue.ErrorMessages.Should().NotBeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldRequestRentalClosureAsyncReturnOk))]
        [Trait("WebApi", "RentalControllerTests")]
        public async Task ShouldRequestRentalClosureAsyncReturnOk()
        {
            //arrange
            var input = _fixture.Create<RequestMotorcycleRentalClosureInput>();
            var output = new RequestMotorcycleRentalClosureOutput();
            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _controller.RequestRentalClosureAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var resultOutput = (ObjectResult)result;
            resultOutput.Should().NotBeNull();
            resultOutput.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultOutput.Value.Should().BeOfType<RequestMotorcycleRentalClosureOutput>();

            var outputValue = (RequestMotorcycleRentalClosureOutput)resultOutput.Value!;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeTrue();
            outputValue.ErrorMessages.Should().BeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldRequestRentalClosureAsyncReturnBadRequestWhenResultIsInvalid))]
        [Trait("WebApi", "RentalControllerTests")]
        public async Task ShouldRequestRentalClosureAsyncReturnBadRequestWhenResultIsInvalid()
        {
            //arrange
            var input = _fixture.Create<RequestMotorcycleRentalClosureInput>();
            var output = new RequestMotorcycleRentalClosureOutput();
            output.ErrorMessages.Add("fail");

            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _controller.RequestRentalClosureAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var badRequestOutput = (ObjectResult)result;
            badRequestOutput.Should().NotBeNull();
            badRequestOutput.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestOutput.Value.Should().BeOfType<RequestMotorcycleRentalClosureOutput>();

            var outputValue = (RequestMotorcycleRentalClosureOutput)badRequestOutput.Value!;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeFalse();
            outputValue.ErrorMessages.Should().NotBeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

    }
}
