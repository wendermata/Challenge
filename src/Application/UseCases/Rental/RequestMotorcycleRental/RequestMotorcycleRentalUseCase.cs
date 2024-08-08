using Application.Common;
using Application.UseCases.Rental.RequestMotorcycleRental.Inputs;
using Application.UseCases.Rental.RequestMotorcycleRental.Mapping;
using Application.UseCases.Rental.RequestRentMotorcycle;
using Domain.Repository;

namespace Application.UseCases.Rental.RequestMotorcycleRental
{
    public class RequestMotorcycleRentalUseCase : IRequestMotorcycleRentalUseCase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IRenterRepository _renterRepository;

        public RequestMotorcycleRentalUseCase(IRentalRepository rentalRepository, IRenterRepository renterRepository)
        {
            _rentalRepository = rentalRepository;
            _renterRepository = renterRepository;
        }

        public async Task<Output> Handle(RequestMotorcycleRentalInput request, CancellationToken cancellationToken)
        {
            Output output = new();
            try
            {
                var renter = await _renterRepository.GetByIdAsync(request.RenterId, cancellationToken);
                if (renter.CanRent() is false)
                {
                    output.ErrorMessages.Add($"Renter {request.RenterId} can't rent motorcycle.");
                    return output;
                }
                var rental = request.MapToDomain();

                await _rentalRepository.InsertAsync(rental, cancellationToken);
                output.Messages.Add($"Rental {rental.Id} created.");
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
