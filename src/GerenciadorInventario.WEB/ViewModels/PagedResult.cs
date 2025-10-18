namespace GerenciadorInventario.WEB.ViewModels;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; } = 0;

    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
}
