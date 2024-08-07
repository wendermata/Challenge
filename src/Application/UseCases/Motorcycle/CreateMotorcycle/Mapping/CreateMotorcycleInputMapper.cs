using Application.UseCases.Motorcycle.CreateMotorcycle.Inputs;
using Domain.Entities;

namespace Application.UseCases.Motorcycle.CreateMotorcycle.Mapping
{
    public static class CreateMotorcycleInputMapper
    {
        public static Motorcycle MapToDomain(this CreateMotorcycleInput input)
        {
            if (input == null)
                return null;

            return new Motorcycle(Guid.NewGuid(),
                input.Year,
                input.Model,
                input.Plate);
        }
    }
}
