using Application.UseCases.CreateRenter.Inputs;
using Domain.Entities;

namespace Application.UseCases.CreateRenter.Mapping
{
    public static class CreateRenterInputMapper
    {
        public static Renter MapToDomain(this CreateRenterInput input)
        {
            if (input is null)
                return null;

            return new Renter(Guid.NewGuid(),
                input.Name,
                input.Document,
                input.BirthDate,
                input.LicenseNumber,
                input.LicenseType);
        }
    }
}
