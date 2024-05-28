using Group06_Project.Domain.Models;

namespace Group06_Project.Domain.ViewModels;

public class HomeViewModel
{
    public IEnumerable<GenreHomeModel> Genres { get; set; } = new List<GenreHomeModel>();
}