using Application.Interfaces;
using Application.UseCases.CreateMotorcycle;
using Domain.Repository;

namespace UnitTests.Application.UseCases.CreateMotorcycle
{
    public class CreateMotorcycleUseCaseTests
    {
        private readonly IFixture _fixture;

        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly CreateMotorcycleUseCase _useCase;

        public CreateMotorcycleUseCaseTests()
        {
            _fixture = new Fixture();
            _motorcycleRepository = Substitute.For<IMotorcycleRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            _useCase = new CreateMotorcycleUseCase(_motorcycleRepository, _unitOfWork);
        }
    }
}
