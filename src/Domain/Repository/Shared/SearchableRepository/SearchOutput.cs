namespace Domain.Repository.Shared.SearchableRepository;

public class SearchOutput<T>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<T> Items { get; set; }
    public SearchOutput(
        int currentPage,
        int pageSize,
        int total,
        IReadOnlyList<T> items)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        Total = total;
        Items = items;
    }
}
