using Application.Common;
using Application.UseCases.Motorcycle.DeleteMotorcycle.Inputs;
using Domain.Repository;

namespace Application.UseCases.Motorcycle.DeleteMotorcycle
{
    public class DeleteMotorcycleUseCase : IDeleteMotorcycleUseCase
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalRepository _rentalRepository;

        public DeleteMotorcycleUseCase(IMotorcycleRepository motorcycleRepository,
            IRentalRepository rentalRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<Output> Handle(DeleteMotorcycleInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                if (request == null)
                {
                    output.ErrorMessages.Add($"Invalid request: {request}");
                    return output;
                }

                var motorcycle = await _motorcycleRepository.GetByIdAsync(request.Id, cancellationToken);
                if (motorcycle is null)
                {
                    output.ErrorMessages.Add($"Motorcycle with Id: {request.Id} not found!");
                    return output;
                }

                var rentals = await _rentalRepository.GetRentalsByMotorcycleId(request.Id, cancellationToken);
                if (rentals.Count > 0)
                {
                    output.ErrorMessages.Add($"Motorcycle with Id: {request.Id} can't be deleted because it was already used in past rentals!");
                    return output;
                }

                motorcycle.Delete();
                await _motorcycleRepository.UpdateAsync(motorcycle, cancellationToken);

                output.Messages.Add($"Motorcycle with Id: {request.Id} successfully deleted");
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
