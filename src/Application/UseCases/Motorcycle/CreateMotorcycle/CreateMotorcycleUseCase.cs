using Application.UseCases.Motorcycle.CreateMotorcycle.Inputs;
using Application.UseCases.Motorcycle.CreateMotorcycle.Mapping;
using Application.UseCases.Motorcycle.CreateMotorcycle.Outputs;
using Domain.Repository;

namespace Application.UseCases.Motorcycle.CreateMotorcycle
{
    public class CreateMotorcycleUseCase : ICreateMotorcycleUseCase
    {
        private readonly IMotorcycleRepository _repository;

        public CreateMotorcycleUseCase(IMotorcycleRepository motorcycleRepository)
        {
            _repository = motorcycleRepository;
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

                if (await _repository.CheckIfExistsAsync(domain.Plate, cancellationToken))
                {
                    output.ErrorMessages.Add($"{domain.Plate} already registered in database");
                    return output;
                }

                await _repository.InsertAsync(domain, cancellationToken);

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
