using Application.Common;
using Application.UseCases.Renter.UploadRenterLicenseImage.Inputs;
using Domain.Repository;

namespace Application.UseCases.Renter.UploadRenterLicenseImage
{
    public class UploadRenterLicenseImageUseCase : IUploadRenterLicenseImageUseCase
    {
        private readonly IRenterRepository _renterRepository;

        public UploadRenterLicenseImageUseCase(IRenterRepository renterRepository)
        {
            _renterRepository = renterRepository;
        }

        public async Task<Output> Handle(UploadRenterLicenseImageInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                var renter = await _renterRepository.GetByIdAsync(request.RenterId, cancellationToken);

                //blob -> aws s3

                var url = "";

                renter.UploadLicenseImage(url);
                await _renterRepository.UpdateAsync(renter, cancellationToken);

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
