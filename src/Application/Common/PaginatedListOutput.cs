namespace Application.Common;
public abstract class PaginatedListOutput<T> : Output
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<T> Items { get; set; }

    protected PaginatedListOutput(
        int page, 
        int pageSize, 
        int total, 
        IReadOnlyList<T> items)
    {
        Page = page;
        PageSize = pageSize;
        Total = total;
        Items = items;
    }
    protected PaginatedListOutput(){}
}
