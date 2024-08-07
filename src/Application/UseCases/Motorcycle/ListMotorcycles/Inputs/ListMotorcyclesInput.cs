using Application.Common;
using Application.UseCases.Motorcycle.ListMotorcycles.Outputs;
using Domain.Repository.Shared.SearchableRepository;
using MediatR;

namespace Application.UseCases.Motorcycle.ListMotorcycles.Inputs
{
    public class ListMotorcyclesInput : PaginatedListInput, IRequest<ListMotorcyclesOutput>
    {
        public ListMotorcyclesInput(
            int page = 1,
            int pageSize = 15,
            string search = "",
            string sort = "",
            SearchOrder dir = SearchOrder.Asc) : base(page, pageSize, search, sort, dir)
        { }

        public ListMotorcyclesInput() : base(1, 15, "", "", SearchOrder.Asc)
        { }
    }
}
