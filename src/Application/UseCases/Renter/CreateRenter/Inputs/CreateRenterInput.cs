using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Renter.CreateRenter.Inputs
{
    public class CreateRenterInput : IRequest<Output>
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
        public string LicenseNumber { get; set; }
        public LicenseType LicenseType { get; set; }
    }
}
