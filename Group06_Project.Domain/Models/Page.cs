using System.Text.Json;

namespace Group06_Project.Domain.Models;

public class Page<T> where T : class
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public long TotalElement { get; init; }
    public int TotalPage => (int)Math.Ceiling((double)TotalElement / PageSize);
    public string? Sort { get; init; }
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPage;
    public IEnumerable<T> Data { get; init; } = Enumerable.Empty<T>();

    public override string ToString()
    {
        var json = new
        {
            PageNumber,
            PageSize,
            TotalElement,
            TotalPage,
            Sort,
            HasPrevious,
            HasNext
        };
        return JsonSerializer.Serialize(json);
    }
}