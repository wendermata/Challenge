using Application.Interfaces;
using Application.UseCases.DeleteMotorcycle;
using Application.UseCases.DeleteMotorcycle.Inputs;
using Domain.Entities;
using Domain.Repository;

namespace UnitTests.Application.UseCases.DeleteMotorcycle
{
    public class DeleteMotorcycleUseCaseTests
    {
        private readonly IFixture _fixture;
        private readonly CancellationToken _cancellationToken;

        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly DeleteMotorcycleUseCase _useCase;

        public DeleteMotorcycleUseCaseTests()
        {
            _fixture = new Fixture();
            _cancellationToken = CancellationToken.None;

            _motorcycleRepository = Substitute.For<IMotorcycleRepository>();
            _rentalRepository = Substitute.For<IRentalRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            _useCase = new DeleteMotorcycleUseCase(_motorcycleRepository, _rentalRepository, _unitOfWork);
        }

        [Fact(DisplayName = nameof(ShouldFailWhenRequestIsInvalid))]
        [Trait("Application", "DeleteMotorcycleUseCase")]
        public async Task ShouldFailWhenRequestIsInvalid()
        {
            //arrange
            DeleteMotorcycleInput input = null;

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().Contain(x => x.StartsWith("Invalid request"));
        }

        [Fact(DisplayName = nameof(ShouldFailWhenIdIsNotFound))]
        [Trait("Application", "DeleteMotorcycleUseCase")]
        public async Task ShouldFailWhenIdIsNotFound()
        {
            //arrange
            var input = _fixture.Create<DeleteMotorcycleInput>();

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().Contain(x => x.Equals($"Motorcycle with Id: {input.Id} not found!"));
        }

        [Fact(DisplayName = nameof(ShouldFailWhenIdIsWasAlreadyUsedInPastRentals))]
        [Trait("Application", "DeleteMotorcycleUseCase")]
        public async Task ShouldFailWhenIdIsWasAlreadyUsedInPastRentals()
        {
            //arrange
            var input = _fixture.Create<DeleteMotorcycleInput>();
            var moto = new Motorcycle(input.Id,
                2019,
                _fixture.Create<string>(),
                _fixture.Create<string>()[..7],
                _fixture.Create<DateTime>(),
                _fixture.Create<DateTime>(),
                true);

            var rentals = _fixture.CreateMany<Rental>(4).ToList();

            _motorcycleRepository.GetByIdAsync(Arg.Is(input.Id), _cancellationToken).Returns(moto);
            _rentalRepository.GetRentalsByMotorcycleId(Arg.Is(input.Id), _cancellationToken).Returns(rentals);

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().Contain(x => x.Equals($"Motorcycle with Id: {input.Id} can't be deleted because it was already used in past rentals!"));
        }

        [Fact(DisplayName = nameof(ShouldFailWhenExceptionIsThrown))]
        [Trait("Application", "DeleteMotorcycleUseCase")]
        public async Task ShouldFailWhenExceptionIsThrown()
        {
            //arrange
            var input = _fixture.Create<DeleteMotorcycleInput>();

            _motorcycleRepository
                .When(x => x.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()))
                .Do(x =>
                {
                    throw new Exception("fail");
                });

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().NotBeNullOrEmpty();
        }

        [Fact(DisplayName = nameof(ShouldSuccess))]
        [Trait("Application", "DeleteMotorcycleUseCase")]
        public async Task ShouldSuccess()
        {
            //arrange
            var input = _fixture.Create<DeleteMotorcycleInput>();
            var moto = new Motorcycle(input.Id,
                2019,
                _fixture.Create<string>(),
                _fixture.Create<string>()[..7],
                _fixture.Create<DateTime>(),
                _fixture.Create<DateTime>(),
                true);

            List<Rental> rentals = new();

            _motorcycleRepository.GetByIdAsync(Arg.Is(input.Id), _cancellationToken).Returns(moto);
            _rentalRepository.GetRentalsByMotorcycleId(Arg.Is(input.Id), _cancellationToken).Returns(rentals);

            //act
            var result = await _useCase.Handle(input, _cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeNullOrEmpty();
            result.Messages.Should().Contain(x => x.Equals($"Motorcycle with Id: {input.Id} successfully deleted"));
        }
    }
}
