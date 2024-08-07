using Application.UseCases.Renter.CreateRenter.Inputs;
using DomainRenter = Domain.Entities.Renter;

namespace Application.UseCases.Renter.CreateRenter.Mapping
{
    public static class CreateRenterInputMapper
    {
        public static DomainRenter MapToDomain(this CreateRenterInput input)
        {
            if (input is null)
                return null;

            return new DomainRenter(Guid.NewGuid(),
                input.Name,
                input.Document,
                input.BirthDate,
                input.LicenseNumber,
                input.LicenseType);
        }
    }
}
