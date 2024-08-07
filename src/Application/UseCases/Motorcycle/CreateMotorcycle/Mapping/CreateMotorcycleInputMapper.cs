using Application.UseCases.Motorcycle.CreateMotorcycle.Inputs;
using DomainMotorcycle = Domain.Entities.Motorcycle;

namespace Application.UseCases.Motorcycle.CreateMotorcycle.Mapping
{
    public static class CreateMotorcycleInputMapper
    {
        public static DomainMotorcycle MapToDomain(this CreateMotorcycleInput input)
        {
            if (input == null)
                return null;

            return new DomainMotorcycle(Guid.NewGuid(),
                input.Year,
                input.Model,
                input.Plate);
        }
    }
}
