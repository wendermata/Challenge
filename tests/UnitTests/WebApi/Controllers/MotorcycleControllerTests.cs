using Application.Common;
using Application.UseCases.CreateMotorcycle.Inputs;
using Application.UseCases.CreateMotorcycle.Outputs;
using Application.UseCases.DeleteMotorcycle.Inputs;
using Application.UseCases.ListMotorcycles.Inputs;
using Application.UseCases.ListMotorcycles.Outputs;
using Application.UseCases.ModifyMotorcyclePlate.Inputs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Controllers;

namespace UnitTests.WebApi.Controllers
{
    public class MotorcycleControllerTests
    {
        private readonly IFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly ILogger<MotorcycleController> _logger;
        private readonly IMediator _mediator;

        private readonly MotorcycleController _controller;

        public MotorcycleControllerTests()
        {
            _fixture = new Fixture();
            _cancellationToken = new CancellationToken();

            _mediator = Substitute.For<IMediator>();
            _logger = Substitute.For<ILogger<MotorcycleController>>();

            _controller = new MotorcycleController(_logger, _mediator);
        }

        [Fact(DisplayName = nameof(ShouldCreateAsyncReturnCreated))]
        [Trait("WebApi", "MotorcycleController")]
        public async Task ShouldCreateAsyncReturnCreated()
        {
            //arrange
            var input = _fixture.Create<CreateMotorcycleInput>();
            var output = new CreateMotorcycleOutput();

            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _controller.CreateAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var badRequestOutput = (ObjectResult)result;
            badRequestOutput.Should().NotBeNull();
            badRequestOutput.StatusCode.Should().Be(StatusCodes.Status201Created);
            badRequestOutput.Value.Should().BeOfType<CreateMotorcycleOutput>();
            
            var outputValue = (CreateMotorcycleOutput)badRequestOutput.Value;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeTrue();
            outputValue.ErrorMessages.Should().BeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldCreateAsyncReturnBadRequestWhenResultIsInvalid))]
        [Trait("WebApi", "MotorcycleController")]
        public async Task ShouldCreateAsyncReturnBadRequestWhenResultIsInvalid()
        {
            //arrange
            var input = _fixture.Create<CreateMotorcycleInput>();

            var output = new CreateMotorcycleOutput();
            output.ErrorMessages.Add("fail");

            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _controller.CreateAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var badRequestOutput = (ObjectResult)result;
            badRequestOutput.Should().NotBeNull();
            badRequestOutput.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestOutput.Value.Should().BeOfType<CreateMotorcycleOutput>();

            var outputValue = (CreateMotorcycleOutput)badRequestOutput.Value;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeFalse();
            outputValue.ErrorMessages.Should().NotBeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldListMotorcyclesAsyncReturnOk))]
        [Trait("WebApi", "MotorcycleController")]
        public async Task ShouldListMotorcyclesAsyncReturnOk()
        {
            //arrange
            var input = _fixture.Create<ListMotorcyclesInput>();
            var output = new ListMotorcyclesOutput();

            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _controller.ListMotorcyclesAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var badRequestOutput = (ObjectResult)result;
            badRequestOutput.Should().NotBeNull();
            badRequestOutput.StatusCode.Should().Be(StatusCodes.Status200OK);
            badRequestOutput.Value.Should().BeOfType<ListMotorcyclesOutput>();

            var outputValue = (ListMotorcyclesOutput)badRequestOutput.Value;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeTrue();
            outputValue.ErrorMessages.Should().BeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldListMotorcyclesAsyncReturnBadRequestWhenResultIsInvalid))]
        [Trait("WebApi", "MotorcycleController")]
        public async Task ShouldListMotorcyclesAsyncReturnBadRequestWhenResultIsInvalid()
        {
            //arrange
            var input = _fixture.Create<ListMotorcyclesInput>();

            var output = new ListMotorcyclesOutput();
            output.ErrorMessages.Add("fail");

            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _controller.ListMotorcyclesAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var badRequestOutput = (ObjectResult)result;
            badRequestOutput.Should().NotBeNull();
            badRequestOutput.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestOutput.Value.Should().BeOfType<ListMotorcyclesOutput>();

            var outputValue = (ListMotorcyclesOutput)badRequestOutput.Value;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeFalse();
            outputValue.ErrorMessages.Should().NotBeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldModifyMotorcycleAsyncReturnOk))]
        [Trait("WebApi", "MotorcycleController")]
        public async Task ShouldModifyMotorcycleAsyncReturnOk()
        {
            //arrange
            var input = _fixture.Create<ModifyMotorcyclePlateInput>();
            var output = new Output();

            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _controller.ModifyMotorcycleAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var badRequestOutput = (ObjectResult)result;
            badRequestOutput.Should().NotBeNull();
            badRequestOutput.StatusCode.Should().Be(StatusCodes.Status200OK);
            badRequestOutput.Value.Should().BeOfType<Output>();

            var outputValue = (Output)badRequestOutput.Value;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeTrue();
            outputValue.ErrorMessages.Should().BeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldModifyMotorcycleAsyncReturnBadRequestWhenResultIsInvalid))]
        [Trait("WebApi", "MotorcycleController")]
        public async Task ShouldModifyMotorcycleAsyncReturnBadRequestWhenResultIsInvalid()
        {
            //arrange
            var input = _fixture.Create<ModifyMotorcyclePlateInput>();

            var output = new Output();
            output.ErrorMessages.Add("fail");

            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _controller.ModifyMotorcycleAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var badRequestOutput = (ObjectResult)result;
            badRequestOutput.Should().NotBeNull();
            badRequestOutput.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestOutput.Value.Should().BeOfType<Output>();

            var outputValue = (Output)badRequestOutput.Value;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeFalse();
            outputValue.ErrorMessages.Should().NotBeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldRemoveMotorcycleAsyncReturnOk))]
        [Trait("WebApi", "MotorcycleController")]
        public async Task ShouldRemoveMotorcycleAsyncReturnOk()
        {
            //arrange
            var input = Guid.NewGuid();
            var output = new Output();

            _mediator.Send(Arg.Is<DeleteMotorcycleInput>(x => x.Id.Equals(input)), _cancellationToken).Returns(output);

            //act
            var result = await _controller.RemoveMotorcycleAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var badRequestOutput = (ObjectResult)result;
            badRequestOutput.Should().NotBeNull();
            badRequestOutput.StatusCode.Should().Be(StatusCodes.Status200OK);
            badRequestOutput.Value.Should().BeOfType<Output>();

            var outputValue = (Output)badRequestOutput.Value;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeTrue();
            outputValue.ErrorMessages.Should().BeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldRemoveMotorcycleAsyncReturnBadRequestWhenResultIsInvalid))]
        [Trait("WebApi", "MotorcycleController")]
        public async Task ShouldRemoveMotorcycleAsyncReturnBadRequestWhenResultIsInvalid()
        {
            //arrange
            var input = Guid.NewGuid();
            var output = new Output();
            output.ErrorMessages.Add("fail");

            _mediator.Send(Arg.Is<DeleteMotorcycleInput>(x => x.Id.Equals(input)), _cancellationToken).Returns(output);

            //act
            var result = await _controller.RemoveMotorcycleAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var badRequestOutput = (ObjectResult)result;
            badRequestOutput.Should().NotBeNull();
            badRequestOutput.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            badRequestOutput.Value.Should().BeOfType<Output>();

            var outputValue = (Output)badRequestOutput.Value;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeFalse();
            outputValue.ErrorMessages.Should().NotBeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }
    }
}
