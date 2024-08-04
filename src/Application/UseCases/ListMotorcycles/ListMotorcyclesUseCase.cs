using Application.Interfaces;
using Application.UseCases.ListMotorcycles.Inputs;
using Application.UseCases.ListMotorcycles.Mapping;
using Application.UseCases.ListMotorcycles.Outputs;
using Domain.Repository;

namespace Application.UseCases.ListMotorcycles
{
    public class ListMotorcyclesUseCase : IListMotorcyclesUseCase
    {
        private readonly IMotorcycleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ListMotorcyclesUseCase(IMotorcycleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ListMotorcyclesOutput> Handle(ListMotorcyclesInput request, CancellationToken cancellationToken)
        {
            var output = new ListMotorcyclesOutput();
            try
            {
                if (request is null || string.IsNullOrEmpty(request.Plate))
                {
                    output.ErrorMessages.Add($"Invalid request: {request}");
                    return output;
                }

                var motorcyclesFound = await _repository.FilterByPlateAsync(request.Plate, cancellationToken);
                if (motorcyclesFound is null)
                {
                    output.ErrorMessages.Add($"No plates founded with plate: {request.Plate}");
                    return output;
                }

                output = motorcyclesFound.MapToOutput();
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
