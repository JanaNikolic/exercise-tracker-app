public class PaginatedListApiModel<T>
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

    public PaginatedListApiModel(IEnumerable<T> items, int currentPage, int pageSize)
    {
        Items = items;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
}