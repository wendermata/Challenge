using Application.UseCases.Motorcycle.ListMotorcycles.Outputs;
using DomainMotorcycle = Domain.Entities.Motorcycle;
using Domain.Repository.Shared.SearchableRepository;

namespace Application.UseCases.Motorcycle.ListMotorcycles.Mapping
{
    public static class ListMotorcyclesOutputMapper
    {
        public static ListMotorcyclesOutput MapToOutput(this SearchOutput<DomainMotorcycle> search)
        {
            if (search is null || search.Items.Count == 0)
                return new ListMotorcyclesOutput();

            return new ListMotorcyclesOutput(search.CurrentPage,
                search.PageSize,
                search.Total,
                search.Items
                    .Select(x => x.MapToItemOutput())
                    .ToList()
            );
        }

        public static MotorcycleOutput MapToItemOutput(this DomainMotorcycle motorcycle)
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
