using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group06_Project.Domain.Models
{
    public class FilmHomeModel
    {
        public int Id { get; init; }
        public string Title { get; set; } = null!;
        public string PosterUrl { get; set; } = null!;
        public decimal AverageRating { get; set; }
        public int TotalView { get; set; }
        public IEnumerable<string> Genres { get; set; } = new List<string>();
    }
}

