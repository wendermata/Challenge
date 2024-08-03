using Application.Interfaces;
using Application.UseCases.CreateMotorcycle.Inputs;
using Application.UseCases.CreateMotorcycle.Mapping;
using Application.UseCases.CreateMotorcycle.Outputs;
using Domain.Repository;

namespace Application.UseCases.CreateMotorcycle
{
    public class CreateMotorcycleUseCase : ICreateMotorcycleUseCase
    {
        private readonly IMotorcycleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMotorcycleUseCase(IMotorcycleRepository motorcycleRepository, IUnitOfWork unitOfWork)
        {
            _repository = motorcycleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateMotorcycleOutput> Handle(CreateMotorcycleInput request, CancellationToken cancellationToken)
        {
            var output = new CreateMotorcycleOutput();
            try
            {
                var domain = request.MapToDomain();
                if (domain == null)
                {
                    output.ErrorMessages.Add($"Invalid request: {request}");
                    return output;
                }

                if (await _repository.ExistsMotorcycleAsync(domain.Plate, cancellationToken))
                {
                    output.ErrorMessages.Add($"{domain.Plate} already registered in database");
                    return output;
                }

                await _repository.InsertAsync(domain, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                output = domain.MapToOutput();
                return output;
            }
            catch (Exception ex)
            {
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}
