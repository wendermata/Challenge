using Application.UseCases.Motorcycle.CreateMotorcycle.Outputs;
using DomainMotorcycle = Domain.Entities.Motorcycle;

namespace Application.UseCases.Motorcycle.CreateMotorcycle.Mapping
{
    public static class CreateMotorcycleOutputMapper
    {
        public static CreateMotorcycleOutput MapToOutput(this DomainMotorcycle domain)
        {
            var output = new CreateMotorcycleOutput();

            if (domain == null)
                return null;

            output.MotorcycleId = domain.Id;
            output.Messages.Add($"Motorcycle with plate {domain.Plate} created successfully. Id: {domain.Id}");

            return output;
        }
    }
}
