﻿namespace Group06_Project.Domain.Models;

public class FilmItemList
{
    public int Id { get; init; }
    public string Title { get; set; } = null!;
    public string PosterUrl { get; set; } = null!;
    public decimal AverageRating { get; set; }
    public int TotalComment { get; set; }
    public int ReleaseYear { get; set; }
    public DateTime CreatedDate { get; set; }
    public int TotalView { get; set; }
    public bool IsVisible { get; set; }
    public IEnumerable<SelectOption> Genres { get; set; } = new List<SelectOption>();
}