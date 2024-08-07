using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Renter.UploadRenterLicenseImage.Inputs
{
    public class UploadRenterLicenseImageInput : IRequest<Output>
    {
        public Guid RenterId { get; set; }
        public IFormFile Image { get; set; }
    }
}
