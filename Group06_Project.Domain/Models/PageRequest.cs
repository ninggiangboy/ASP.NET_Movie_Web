namespace Group06_Project.Domain.Models;

public class PageRequest<T> where T : class
{
    private readonly string? _sort;
    public int PageNumber { get; init; } = 1;
    public int Size { get; init; } = 10;

    public string? Sort
    {
        get => _sort;
        init
        {
            value = value?.ToLower();
            _sort = IsValidSortValue(value) ? value : null;
        }
    }

    // Valid sort value: "id", "id desc", "id asc", "id desc, name asc"
    private static bool IsValidSortValue(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;
        var properties = typeof(T).GetProperties().Select(p => p.Name.ToLower()).ToList();
        foreach (var se in value.Split(","))
        {
            var s = se.Split(" ");
            if (s.Length is not (1 or 2)) return false;
            if (!properties.Contains(s[0])) return false;
            if (s.Length != 1 && (!s[1].Equals("asc") || !s[1].Equals("desc"))) return false;
        }

        return true;
    }
}