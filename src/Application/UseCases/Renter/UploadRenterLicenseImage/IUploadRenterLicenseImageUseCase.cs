using Application.Common;
using Application.UseCases.Renter.UploadRenterLicenseImage.Inputs;
using MediatR;

namespace Application.UseCases.Renter.UploadRenterLicenseImage
{
    public interface IUploadRenterLicenseImageUseCase : IRequestHandler<UploadRenterLicenseImageInput, Output>
    {
    }
}
