using Application.UseCases.Motorcycle.CreateMotorcycle;
using Application.UseCases.Motorcycle.CreateMotorcycle.Inputs;
using Domain.Repository;

namespace UnitTests.Application.UseCases.CreateMotorcycle
{
    public class CreateMotorcycleUseCaseTests
    {
        private readonly IFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly IMotorcycleRepository _motorcycleRepository;

        private readonly CreateMotorcycleUseCase _useCase;

        public CreateMotorcycleUseCaseTests()
        {
            _fixture = new Fixture();
            _cancellationToken = new CancellationToken();

            _motorcycleRepository = Substitute.For<IMotorcycleRepository>();

            _useCase = new CreateMotorcycleUseCase(_motorcycleRepository);
        }

        [Fact(DisplayName = nameof(ShouldFailWhenRequestIsInvalid))]
        [Trait("Application", "CreateMotorcycleUseCase")]
        public async Task ShouldFailWhenRequestIsInvalid()
        {
            //arrange
            CreateMotorcycleInput input = null;

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().Contain(x => x.StartsWith("Invalid request"));
        }

        [Fact(DisplayName = nameof(ShouldFailWhenMotorcycleAlreadyExists))]
        [Trait("Application", "CreateMotorcycleUseCase")]
        public async Task ShouldFailWhenMotorcycleAlreadyExists()
        {
            //arrange
            var input = _fixture.Build<CreateMotorcycleInput>()
                .With(x => x.Year, 2020)
                .With(x => x.Plate, _fixture.Create<string>()[..7])
                .Create();

            _motorcycleRepository.CheckIfExistsAsync(Arg.Is(input.Plate), Arg.Is(_cancellationToken)).Returns(true);

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().Contain(x => x.Contains($"{input.Plate} already registered in database"));
        }

        [Fact(DisplayName = nameof(ShouldFailWhenExceptionIsThrown))]
        [Trait("Application", "CreateMotorcycleUseCase")]
        public async Task ShouldFailWhenExceptionIsThrown()
        {
            //arrange
            var input = _fixture.Build<CreateMotorcycleInput>()
                .With(x => x.Year, 1700)
                .Create();

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().NotBeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldSuccess))]
        [Trait("Application", "CreateMotorcycleUseCase")]
        public async Task ShouldSuccess()
        {
            //arrange
            var input = _fixture.Build<CreateMotorcycleInput>()
                .With(x => x.Year, 2020)
                .With(x => x.Plate, _fixture.Create<string>().Substring(0, 7))
                .Create();

            _motorcycleRepository.CheckIfExistsAsync(Arg.Is(input.Plate), Arg.Is(_cancellationToken)).Returns(false);

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.Messages.Should().NotBeNullOrEmpty();
        }
    }
}
