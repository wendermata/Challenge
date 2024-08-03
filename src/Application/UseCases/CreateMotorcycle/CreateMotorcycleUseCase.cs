using Application.Interfaces;
using Application.UseCases.CreateMotorcycle.Inputs;
using Domain.Repository;

namespace Application.UseCases.CreateMotorcycle
{
    public class CreateMotorcycleUseCase : ICreateMotorcycleUseCase
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMotorcycleUseCase(IMotorcycleRepository motorcycleRepository, IUnitOfWork unitOfWork)
        {
            _motorcycleRepository = motorcycleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateMotorcycleInput request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
