namespace Domain.Repository.Shared.SearchableRepository;

public class SearchInput
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string Search { get; set; }
    public string OrderBy { get; set; }
    public SearchOrder Order { get; set; }

    public SearchInput(
        int page,
        int pageSize,
        string search,
        string orderBy,
        SearchOrder order)
    {
        Page = page;
        PageSize = pageSize;
        Search = search;
        OrderBy = orderBy;
        Order = order;
    }
}
