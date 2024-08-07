using Application.Interfaces;
using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Inputs;
using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Mapping;
using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Outputs;
using Domain.Repository;

namespace Application.UseCases.Rental.RequestMotorcycleRentalClosure
{
    public class RequestMotorcycleRentalClosureUseCase : IRequestMotorcycleRentalClosureUseCase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequestMotorcycleRentalClosureUseCase(IRentalRepository rentalRepository, IUnitOfWork unitOfWork)
        {
            _rentalRepository = rentalRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RequestMotorcycleRentalClosureOutput> Handle(RequestMotorcycleRentalClosureInput request, CancellationToken cancellationToken)
        {
            var output = new RequestMotorcycleRentalClosureOutput();
            try
            {
                var rental = await _rentalRepository.GetByIdAsync(request.RentalId, cancellationToken);
                if (rental == null)
                {
                    output.ErrorMessages.Add($"Rental with id {request.RentalId} not found");
                    return output;
                }

                if (rental.IsFinished)
                {
                    output.ErrorMessages.Add($"Rental with id {request.RentalId} was already closed in {rental.DevolutionDate!.Value:YYYY-mm-dd}");
                    return output;
                }

                rental.FinishRental(request.ClosureDate);
                await _rentalRepository.UpdateAsync(rental, cancellationToken);
                await _unitOfWork.Commit(cancellationToken);

                output = rental.MapToOutput();
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
