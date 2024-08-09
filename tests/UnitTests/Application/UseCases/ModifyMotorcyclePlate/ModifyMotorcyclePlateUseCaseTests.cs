using Application.UseCases.Motorcycle.ModifyMotorcyclePlate;
using Application.UseCases.Motorcycle.ModifyMotorcyclePlate.Inputs;
using Domain.Entities;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace UnitTests.Application.UseCases.ModifyMotorcyclePlate
{
    public class ModifyMotorcyclePlateUseCaseTests
    {
        private readonly IFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly IMotorcycleRepository _repository;
        private readonly ILogger<ModifyMotorcyclePlateUseCase> _logger;

        private readonly ModifyMotorcyclePlateUseCase _useCase;

        public ModifyMotorcyclePlateUseCaseTests()
        {
            _fixture = new Fixture();
            _cancellationToken = new CancellationToken();

            _repository = Substitute.For<IMotorcycleRepository>();
            _logger = Substitute.For<ILogger<ModifyMotorcyclePlateUseCase>>();

            _useCase = new ModifyMotorcyclePlateUseCase(_repository, _logger);
        }

        [Fact(DisplayName = nameof(ShouldFailWhenMotorcycleIsNotFound))]
        [Trait("Application", "ModifyMotorcyclePlateUseCase")]
        public async Task ShouldFailWhenMotorcycleIsNotFound()
        {
            //arrange
            var input = _fixture.Create<ModifyMotorcyclePlateInput>();
            Motorcycle moto = null;
            _repository.GetByIdAsync(Arg.Is<Guid>(x => x.Equals(input.Id)), _cancellationToken).Returns(moto);

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().Contain(x => x.Equals($"Motorcycle with Id: {input.Id} not found!"));
        }

        [Fact(DisplayName = nameof(ShouldFailWhenMotorcycleIsNotFound))]
        [Trait("Application", "ModifyMotorcyclePlateUseCase")]
        public async Task shouldFailWhenPlateWasAlreadyUsed()
        {
            //arrange
            var input = _fixture.Create<ModifyMotorcyclePlateInput>();

            var moto = _fixture.Create<Motorcycle>();
            _repository.GetByIdAsync(Arg.Is<Guid>(x => x.Equals(input.Id)), _cancellationToken).Returns(moto);

            _repository.CheckIfExistsAsync(Arg.Is(input.NewPlate), _cancellationToken).Returns(true);

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().Contain(x => x.Equals($"{input.NewPlate} already registered in database"));
        }

        [Fact(DisplayName = nameof(ShouldSuccess))]
        [Trait("Application", "ModifyMotorcyclePlateUseCase")]
        public async Task ShouldSuccess()
        {
            //arrange
            var input = _fixture.Build<ModifyMotorcyclePlateInput>()
                .With(x => x.NewPlate, _fixture.Create<string>()[..7])
                .Create();

            Motorcycle moto = new Motorcycle(input.Id, 
                2020, 
                _fixture.Create<string>(),
                _fixture.Create<string>()[..7], 
                _fixture.Create<DateTime>(), 
                _fixture.Create<DateTime>(), 
                true);


            _repository.GetByIdAsync(Arg.Is<Guid>(x => x.Equals(input.Id)), _cancellationToken).Returns(moto);
            _repository.CheckIfExistsAsync(Arg.Is(input.NewPlate), _cancellationToken).Returns(false);

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.Messages.Should().Contain(x => x.Equals($"Motorcycle with Id: {input.Id} successfully update plate with: {input.NewPlate}"));
        }
    }
}
