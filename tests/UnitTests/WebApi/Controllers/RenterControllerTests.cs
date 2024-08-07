using Application.Common;
using Application.UseCases.Renter.CreateRenter.Inputs;
using Application.UseCases.Renter.UploadRenterLicenseImage.Inputs;
using AutoFixture.AutoMoq;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Controllers;

namespace UnitTests.WebApi.Controllers
{
    public class RenterControllerTests
    {
        private readonly IFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly ILogger<RenterController> _logger;
        private readonly IMediator _mediator;

        private readonly RenterController _renterController;

        public RenterControllerTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _cancellationToken = new CancellationToken();

            _logger = Substitute.For<ILogger<RenterController>>();
            _mediator = Substitute.For<IMediator>();

            _renterController = new RenterController(_logger, _mediator);
        }

        [Fact(DisplayName = nameof(ShouldCreateAsyncReturnCreated))]
        [Trait("WebApi", "RenterControllerTests")]
        public async Task ShouldCreateAsyncReturnCreated()
        {
            //arrange
            var input = _fixture.Create<CreateRenterInput>();
            var output = new Output();

            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _renterController.CreateAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var resultOutput = (ObjectResult)result;
            resultOutput.Should().NotBeNull();
            resultOutput.StatusCode.Should().Be(StatusCodes.Status201Created);
            resultOutput.Value.Should().BeOfType<Output>();

            var outputValue = (Output)resultOutput.Value;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeTrue();
            outputValue.ErrorMessages.Should().BeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldCreateAsyncReturnBadRequestWhenResultIsInvalid))]
        [Trait("WebApi", "RenterControllerTests")]
        public async Task ShouldCreateAsyncReturnBadRequestWhenResultIsInvalid()
        {
            //arrange
            var input = _fixture.Create<CreateRenterInput>();

            var output = new Output();
            output.ErrorMessages.Add("fail");

            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _renterController.CreateAsync(input, _cancellationToken);

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

        [Fact(DisplayName = nameof(ShouldUploadLicenseImageAsyncAsyncReturnOk))]
        [Trait("WebApi", "RenterControllerTests")]
        public async Task ShouldUploadLicenseImageAsyncAsyncReturnOk()
        {
            //arrange
            UploadRenterLicenseImageInput input = new() { RenterId = Guid.NewGuid(), Image = GetMockFormFile() };
            
            var output = new Output();
            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _renterController.UploadLicenseImageAsync(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();

            var resultOutput = (ObjectResult)result;
            resultOutput.Should().NotBeNull();
            resultOutput.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultOutput.Value.Should().BeOfType<Output>();

            var outputValue = (Output)resultOutput.Value;
            outputValue.Should().NotBeNull();
            outputValue!.IsValid.Should().BeTrue();
            outputValue.ErrorMessages.Should().BeNullOrEmpty();
            outputValue.Messages.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldUploadLicenseImageAsyncReturnBadRequestWhenResultIsInvalid))]
        [Trait("WebApi", "RenterControllerTests")]
        public async Task ShouldUploadLicenseImageAsyncReturnBadRequestWhenResultIsInvalid()
        {
            //arrange
            UploadRenterLicenseImageInput input = new() { RenterId = Guid.NewGuid(), Image = GetMockFormFile() };

            var output = new Output();
            output.ErrorMessages.Add("fail");

            _mediator.Send(Arg.Is(input), _cancellationToken).Returns(output);

            //act
            var result = await _renterController.UploadLicenseImageAsync(input, _cancellationToken);

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

        private IFormFile GetMockFormFile()
        {
            var fileName = _fixture.Create<string>() + "jpg";
            var content = _fixture.Create<string>();
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            var formFile = new FormFile(stream, 0, stream.Length, "teste", fileName);

            return formFile;
        }
    }
}
