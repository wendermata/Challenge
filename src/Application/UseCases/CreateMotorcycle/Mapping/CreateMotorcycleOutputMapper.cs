using Application.UseCases.CreateMotorcycle.Outputs;
using Domain.Entities;

namespace Application.UseCases.CreateMotorcycle.Mapping
{
    public static class CreateMotorcycleOutputMapper
    {
        public static CreateMotorcycleOutput MapToOutput(this Motorcycle domain)
        {
            var output = new CreateMotorcycleOutput();

            if (domain == null)
                return null;

            output.MotorcycleId = domain.Id;
            output.Messages.Add($"Motorcycle with plate {domain.Plate} was inserted successfully. Id: {domain.Id}");

            return output;
        }
    }
}
