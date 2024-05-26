using Group06_Project.Domain.Interfaces;

namespace Group06_Project.Infrastructure.Data;

public class DbInitializer : IDbInitializer
{
    // private readonly ICountryRepository _countryRepository;
    // private readonly IGenreRepository _genreRepository;
    // private readonly RoleManager<IdentityRole> _roleManager;
    // private readonly IUnitOfWork _unitOfWork;
    // private readonly UserManager<IdentityUser> _userManager;
    //
    // public DbInitializer(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager,
    //     ICountryRepository countryRepository, IUnitOfWork unitOfWork, IGenreRepository genreRepository)
    // {
    //     _roleManager = roleManager;
    //     _userManager = userManager;
    //     _countryRepository = countryRepository;
    //     _unitOfWork = unitOfWork;
    //     _genreRepository = genreRepository;
    // }
    //
    // public async void Initialize()
    // {
    //     await InitializeRoles();
    //     await InitializeAdmin();
    //     await InitializeCountries();
    //     await InitializeGenres();
    // }
    //
    // private async Task InitializeRoles()
    // {
    //     var adminReady = await _roleManager.RoleExistsAsync(UserRoles.Admin);
    //     var userReady = await _roleManager.RoleExistsAsync(UserRoles.User);
    //
    //     if (!adminReady) await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
    //
    //     if (!userReady) await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
    // }
    //
    // private async Task InitializeAdmin()
    // {
    //     if (await _userManager.FindByNameAsync("admin") != null) return;
    //     var admin = new IdentityUser
    //     {
    //         UserName = "admin",
    //         Email = "ninggiangboy@gmail.com",
    //         EmailConfirmed = true
    //     };
    //     await _userManager.CreateAsync(admin, "khanh.ha");
    //     await _userManager.AddToRoleAsync(admin, UserRoles.Admin);
    // }
    //
    // private Task InitializeCountries()
    // {
    //     if (_countryRepository.Count() > 0) return Task.CompletedTask;
    //     var countries = new List<Country>
    //     {
    //         new() { Name = "Vietnam" },
    //         new() { Name = "United States" },
    //         new() { Name = "Japan" },
    //         new() { Name = "Korea" },
    //         new() { Name = "China" }
    //     };
    //     _countryRepository.AddRange(countries);
    //     _unitOfWork.Commit();
    //     return Task.CompletedTask;
    // }
    //
    // private Task InitializeGenres()
    // {
    //     if (_genreRepository.Count() > 0) return Task.CompletedTask;
    //     var genres = new List<Genre>
    //     {
    //         new() { Name = "Action" },
    //         new() { Name = "Adventure" },
    //         new() { Name = "Comedy" },
    //         new() { Name = "Drama" },
    //         new() { Name = "Fantasy" },
    //         new() { Name = "Horror" },
    //         new() { Name = "Mystery" },
    //         new() { Name = "Psychological" },
    //         new() { Name = "Romance" },
    //         new() { Name = "Sci-fi" },
    //         new() { Name = "Slice of Life" },
    //         new() { Name = "Supernatural" }
    //     };
    //     _genreRepository.AddRange(genres);
    //     _unitOfWork.Commit();
    //     return Task.CompletedTask;
    // }
    public void Initialize()
    {
        throw new NotImplementedException();
    }
}