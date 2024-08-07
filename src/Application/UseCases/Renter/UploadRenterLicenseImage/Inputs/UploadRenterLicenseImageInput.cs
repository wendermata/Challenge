using Application.Common;
using MediatR;
using System.Reflection.Metadata;

namespace Application.UseCases.Renter.UploadRenterLicenseImage.Inputs
{
    public class UploadRenterLicenseImageInput : IRequest<Output>
    {
        public Guid RenterId { get; set; }
        public Blob Image { get; set; }
    }
}
