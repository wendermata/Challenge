using Application.UseCases.ListMotorcycles.Outputs;
using Domain.Entities;

namespace Application.UseCases.ListMotorcycles.Mapping
{
    public static class ListMotorcyclesOutputMapper
    {
        public static ListMotorcyclesOutput MapToOutput(this List<Motorcycle> list)
        {
            var output = new ListMotorcyclesOutput();
            if (list is null || list.Count == 0)
                return null;

            foreach (var motorcycle in list)
                output.Motorcycles.Add(motorcycle.MapToItemOutput());

            return output;
        }

        public static MotorcycleOutput MapToItemOutput(this Motorcycle motorcycle)
        {
            if (motorcycle is null)
                return null;

            return new MotorcycleOutput()
            {
                Id = motorcycle.Id,
                Model = motorcycle.Model,
                Plate = motorcycle.Plate,
                Year = motorcycle.Year
            };
        }
    }
}
