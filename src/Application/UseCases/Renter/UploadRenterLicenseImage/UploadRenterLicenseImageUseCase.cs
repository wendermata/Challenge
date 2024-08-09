using Application.Boundaries.Services.S3;
using Application.Common;
using Application.UseCases.Renter.UploadRenterLicenseImage.Inputs;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Renter.UploadRenterLicenseImage
{
    public class UploadRenterLicenseImageUseCase : IUploadRenterLicenseImageUseCase
    {
        private readonly IRenterRepository _renterRepository;
        private readonly IS3Service _s3Service;
        private readonly ILogger<UploadRenterLicenseImageUseCase> _logger;

        public UploadRenterLicenseImageUseCase(IRenterRepository renterRepository, IS3Service s3Service, ILogger<UploadRenterLicenseImageUseCase> logger)
        {
            _renterRepository = renterRepository;
            _s3Service = s3Service;
            _logger = logger;
        }

        public async Task<Output> Handle(UploadRenterLicenseImageInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                var renter = await _renterRepository.GetByIdAsync(request.RenterId, cancellationToken);
                if (renter is null)
                {
                    _logger.LogWarning($"Renter {request.RenterId} not found");
                    output.ErrorMessages.Add($"Renter {request.RenterId} not found");
                    return output;
                }

                if (renter.LicenseImageFileName is null)
                {
                    var extension = Path.GetExtension(request.Image.FileName);
                    renter.GetFriendlyLicenseImage(extension);
                    await _s3Service.UploadFileAsync(request.Image, renter.LicenseImageFileName);
                }
                else
                    await _s3Service.ReplaceFileAsync(request.Image, renter.LicenseImageFileName);

                await _renterRepository.UpdateAsync(renter, cancellationToken);
                _logger.LogInformation($"Renter {renter.Id} license image uploaded.");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An error occurred while uploading renter license image: {ex.Message}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}
