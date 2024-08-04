using Domain.Enums;
using Domain.Repository.Shared.SearchableRepository;

namespace Application.Common;
public abstract class PaginatedListInput
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string Search { get; set; }
    public string Sort { get; set; }
    public SearchOrder Dir { get; set; }

    public PaginatedListInput(
        int page, 
        int pageSize, 
        string search, 
        string sort, 
        SearchOrder dir)
    {
        Page = page;
        PageSize = pageSize;
        Search = search;
        Sort = sort;
        Dir = dir;
    }
}
