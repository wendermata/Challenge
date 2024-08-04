using Application.Common;
using Domain.Repository.Shared.SearchableRepository;

namespace Application.UseCases.ListMotorcycles.Outputs
{
    public class ListMotorcyclesOutput : PaginatedListOutput<MotorcycleOutput>
    {
        public ListMotorcyclesOutput(
            int page,
            int pageSize,
            int total,
            IReadOnlyList<MotorcycleOutput> items) : base(page, pageSize, total, items)
        { }

        public ListMotorcyclesOutput() { }
    }
}
