namespace Group06_Project.Domain.Models;

public class Page<T> where T : class
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public long TotalElement { get; init; }
    public long TotalPage => (int)Math.Ceiling((double)TotalElement / PageSize);
    public string? Sort { get; init; }
    public IEnumerable<T>? Data { get; init; }
}