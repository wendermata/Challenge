using Application.Common;
using Application.Interfaces;
using Application.UseCases.DeleteMotorcycle.Inputs;
using Domain.Repository;

namespace Application.UseCases.DeleteMotorcycle
{
    public class DeleteMotorcycleUseCase : IDeleteMotorcycleUseCase
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMotorcycleUseCase(IMotorcycleRepository motorcycleRepository,
            IRentalRepository rentalRepository,
            IUnitOfWork unitOfWork)
        {
            _motorcycleRepository = motorcycleRepository;
            _rentalRepository = rentalRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Output> Handle(DeleteMotorcycleInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                var motorcycle = await _motorcycleRepository.GetByIdAsync(request.Id, cancellationToken);
                if (motorcycle is null)
                {
                    output.ErrorMessages.Add($"Motorcycle with Id: {request.Id} not found!");
                    return output;
                }

                var rentals = await _rentalRepository.GetRentalsByMotorcycleId(request.Id, cancellationToken);
                if (rentals.Count > 0) 
                {
                    output.ErrorMessages.Add($"Motorcycle with Id: {request.Id} can't be deleted because it's was already used in past rentals!");
                    return output;
                }

                motorcycle.Delete();
                await _motorcycleRepository.UpdateAsync(motorcycle, cancellationToken);
                await _unitOfWork.Commit(cancellationToken);

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
