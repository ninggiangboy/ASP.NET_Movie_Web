using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

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
        Console.WriteLine("Initialize done");
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
            Balance = 1_000_000_000
        };
        await _userManager.CreateAsync(admin, "Admin@123");
        await _userManager.AddToRoleAsync(admin, UserRoles.Admin);
    }

    private Task InitializeCountries()
    {
        if (_unitOfWork.Countries.Count() > 0) return Task.CompletedTask;
        var countries = new List<Country>
        {
            new() { Name = "Vietnam" },
            new() { Name = "United States" },
            new() { Name = "Japan" },
            new() { Name = "Korea" },
            new() { Name = "China" }
        };
        _unitOfWork.Countries.AddAll(countries);
        _unitOfWork.Commit();
        return Task.CompletedTask;
    }

    private Task InitializeGenres()
    {
        if (_unitOfWork.Genres.Count() > 0) return Task.CompletedTask;
        var genres = new List<Genre>
        {
            new() { Name = "Action" },
            new() { Name = "Adventure" },
            new() { Name = "Comedy" },
            new() { Name = "Drama" },
            new() { Name = "Fantasy" },
            new() { Name = "Horror" },
            new() { Name = "Mystery" },
            new() { Name = "Psychological" },
            new() { Name = "Romance" },
            new() { Name = "Sci-fi" },
            new() { Name = "Slice of Life" },
            new() { Name = "Supernatural" }
        };
        _unitOfWork.Genres.AddAll(genres);
        _unitOfWork.Commit();
        return Task.CompletedTask;
    }
}