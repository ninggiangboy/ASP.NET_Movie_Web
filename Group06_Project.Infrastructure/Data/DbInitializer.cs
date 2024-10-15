using Bogus;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using NuGet.Packaging;

namespace Group06_Project.Infrastructure.Data;

public class DbInitializer : IDbInitializer
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public DbInitializer(RoleManager<IdentityRole> roleManager,
        UserManager<User> userManager,
        IUnitOfWork unitOfWork)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async void Initialize()
    {
        await InitializeRoles();
        await InitializeAdmin();
        await InitializeCountries();
        await InitializeGenres();
        await InitializeFilms();
    }

    private async Task InitializeFilms()
    {
        if (_unitOfWork.Films.Count() > 0) return;
        var faker = new Faker<Film>("en")
            .RuleFor(f => f.Title, f => f.Lorem.Sentence())
            .RuleFor(f => f.OtherTitle, f => f.Lorem.Sentence())
            .RuleFor(f => f.Description, f => f.Lorem.Paragraph())
            .RuleFor(f => f.TrailerUrl, f => "https://www.youtube.com/watch?v=dQw4w9WgXcQ")
            .RuleFor(f => f.PosterUrl, f => f.Image.PicsumUrl())
            .RuleFor(f => f.ThumbnailUrl, f => f.Image.PicsumUrl())
            .RuleFor(f => f.Duration, f => f.Random.Number(60, 180))
            .RuleFor(f => f.AverageRating, f => f.Random.Decimal(1, 5))
            .RuleFor(f => f.VideoUrl, f => "https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-1080p.mp4")
            .RuleFor(f => f.Actor, f => f.Person.FullName)
            .RuleFor(f => f.Director, f => f.Person.FullName)
            .RuleFor(f => f.TotalView, f => f.Random.Number(100, 1000))
            .RuleFor(f => f.ReleaseYear, f => f.Random.Number(2000, 2023))
            .RuleFor(f => f.CountryId, f => f.Random.Number(1, 5))
            .RuleFor(f => f.IsVisible, f => f.Random.Bool());
        var films = faker.Generate(1000);
        var genres = _unitOfWork.Genres.GetAll().ToList();
        foreach (var film in films)
        {
            var genreCount = Random.Shared.Next(0, genres.Count - 1);
            var selectedGenres = genres.GetRange(0, genreCount);
            film.Genres.AddRange(selectedGenres);
        }

        _unitOfWork.Films.AddAll(films);
        await _unitOfWork.CommitAsync();
    }

    private async Task InitializeRoles()
    {
        var adminReady = await _roleManager.RoleExistsAsync(UserRoles.Admin);
        var userReady = await _roleManager.RoleExistsAsync(UserRoles.User);

        if (!adminReady) await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

        if (!userReady) await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
    }

    private async Task InitializeAdmin()
    {
        if (await _userManager.FindByNameAsync("admin") != null) return;
        var admin = new User
        {
            UserName = "admin",
            Email = "ninggiangboy@gmail.com",
            EmailConfirmed = true,
            Balance = 1_000
        };
        await _userManager.CreateAsync(admin, "Admin@123");
        await _userManager.AddToRoleAsync(admin, UserRoles.Admin);
    }

    private async Task InitializeCountries()
    {
        if (_unitOfWork.Countries.Count() > 0) return;
        var countries = new List<Country>
        {
            new()
            {
                Name = "Vietnam",
                Image = "https://upload.wikimedia.org/wikipedia/commons/2/21/Flag_of_Vietnam.svg"
            },
            new()
            {
                Name = "United States",
                Image = "https://upload.wikimedia.org/wikipedia/en/a/a4/Flag_of_the_United_States.svg"
            },
            new()
            {
                Name = "Japan",
                Image = "https://upload.wikimedia.org/wikipedia/en/9/9e/Flag_of_Japan.svg"
            },
            new()
            {
                Name = "Korea",
                Image = "https://upload.wikimedia.org/wikipedia/commons/0/09/Flag_of_South_Korea.svg"
            },
            new()
            {
                Name = "China",
                Image =
                    "https://upload.wikimedia.org/wikipedia/commons/f/fa/Flag_of_the_People%27s_Republic_of_China.svg"
            }
        };
        _unitOfWork.Countries.AddAll(countries);
        await _unitOfWork.CommitAsync();
    }

    private async Task InitializeGenres()
    {
        if (_unitOfWork.Genres.Count() > 0) return;
        var genres = new List<Genre>
        {
            new()
            {
                Name = "Action", Image = "https://www.premiumbeat.com/blog/wp-content/uploads/2019/07/John-Wick.jpg"
            },
            new()
            {
                Name = "Adventure",
                Image =
                    "https://unitingartistsorg29644.zapwp.com/q:i/r:1/wp:1/w:375/u:https://unitingartists.org/wp-content/uploads/2020/06/Adventure-Genre-800x445.jpg"
            },
            new()
            {
                Name = "Comedy",
                Image =
                    "https://nofilmschool.com/media-library/comedy-genre.png?id=34067504&width=1200&height=600&coordinates=0%2C0%2C0%2C220"
            },
            new()
            {
                Name = "Drama",
                Image = "https://nofilmschool.com/media-library/drama-genre-crash.jpg?id=34065347&width=800&quality=90"
            },
            new()
            {
                Name = "Fantasy",
                Image =
                    "https://static1.colliderimages.com/wordpress/wp-content/uploads/2021/11/Movies-Like-Lord-of-the-Rings.jpg"
            },
            new()
            {
                Name = "Horror",
                Image = "https://s.studiobinder.com/wp-content/uploads/2020/05/What-is-Horror-StudioBinder.jpg"
            },
            new()
            {
                Name = "Mystery",
                Image = "https://hips.hearstapps.com/hmg-prod/images/gh-101920-murder-mystery-movies-1603137639.png"
            },
            new()
            {
                Name = "Psychological",
                Image = "https://hips.hearstapps.com/hmg-prod/images/81svdo6wcrl-ac-sl1500-1595954187.jpg"
            },
            new()
            {
                Name = "Romance",
                Image =
                    "https://www.myisense.com/cdn/shop/articles/how-to-keep-the-romance-alive-in-the-bedroom-292426.png?v=1666130581"
            },
            new()
            {
                Name = "Sci-fi",
                Image = "https://www.looper.com/img/gallery/best-sci-fi-movies-2023/intro-1701990358.jpg"
            },
            new()
            {
                Name = "Slice of Life",
                Image =
                    "https://static1.colliderimages.com/wordpress/wp-content/uploads/2023/09/slice-of-life-movies.jpg"
            },
            new()
            {
                Name = "Supernatural",
                Image =
                    "https://m.media-amazon.com/images/M/MV5BMmFkNDE1MzItMmNiMy00ZjljLWE1YjItZGRiYzY5MzBiOGEzXkEyXkFqcGdeQXVyMDEyMDU1Mw@@._V1_.jpg"
            }
        };
        _unitOfWork.Genres.AddAll(genres);
        await _unitOfWork.CommitAsync();
    }
}