using Application.Common;
using Application.Interfaces;
using Application.UseCases.Renter.CreateRenter.Inputs;
using Application.UseCases.Renter.CreateRenter.Mapping;
using Domain.Repository;

namespace Application.UseCases.Renter.CreateRenter
{
    public class CreateRenterUseCase : ICreateRenterUseCase
    {
        private readonly IRenterRepository _renterRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRenterUseCase(IRenterRepository renterRepository, IUnitOfWork unitOfWork)
        {
            _renterRepository = renterRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Output> Handle(CreateRenterInput request, CancellationToken cancellationToken)
        {
            Output output = new();
            try
            {
                if (await _renterRepository.GetByDocumentAsync(request.Document, cancellationToken) != null)
                {
                    output.ErrorMessages.Add($"Document '{request.Document}' already registered in database");
                    return output;
                }

                if (await _renterRepository.GetByLicenseAsync(request.LicenseNumber, cancellationToken) != null)
                {
                    output.ErrorMessages.Add($"License number '{request.LicenseNumber}' already registered in database");
                    return output;
                }

                var renter = request.MapToDomain();
                await _renterRepository.InsertAsync(renter, cancellationToken);
                await _unitOfWork.Commit(cancellationToken);

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
