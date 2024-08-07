using Application.UseCases.Motorcycle.ListMotorcycles.Inputs;
using Domain.Repository.Shared.SearchableRepository;

namespace Application.UseCases.Motorcycle.ListMotorcycles.Mapping
{
    public static class ListMotorcyclesInputMapper
    {
        public static SearchInput MapToSearchInput(this ListMotorcyclesInput input)
        {
            if (input == null)
                return null;

            return new SearchInput(input.Page,
                input.PageSize,
                input.Search,
                input.Sort,
                input.Dir);
        }
    }
}
