using Application.Boundaries.Services.S3;
using Application.Common;
using Application.UseCases.Renter.UploadRenterLicenseImage.Inputs;
using Domain.Repository;

namespace Application.UseCases.Renter.UploadRenterLicenseImage
{
    public class UploadRenterLicenseImageUseCase : IUploadRenterLicenseImageUseCase
    {
        private readonly IRenterRepository _renterRepository;
        private readonly IS3Service _s3Service;

        public UploadRenterLicenseImageUseCase(IRenterRepository renterRepository, IS3Service s3Service)
        {
            _renterRepository = renterRepository;
            _s3Service = s3Service;
        }

        public async Task<Output> Handle(UploadRenterLicenseImageInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                var renter = await _renterRepository.GetByIdAsync(request.RenterId, cancellationToken);
                if (renter is null)
                {
                    output.ErrorMessages.Add($"Renter {request.RenterId} not found");
                    return output;
                }

                if (renter.LicenseImageFileName is null)
                {
                    renter.GetFriendlyLicenseImage();
                    await _s3Service.UploadFileAsync(request.Image, renter.LicenseImageFileName);
                }
                else
                    await _s3Service.ReplaceFileAsync(request.Image, renter.LicenseImageFileName);

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
