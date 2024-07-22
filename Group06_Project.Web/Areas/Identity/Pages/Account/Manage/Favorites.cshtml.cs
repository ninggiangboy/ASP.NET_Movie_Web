using System.Security.Claims;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group06_Project.Web.Areas.Identity.Pages.Account.Manage;

public class Favorites : PageModel
{
    private readonly IFavoriteService _favoriteService;

    public Favorites(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    public ICollection<FilmItemList> FavoritesList { get; set; } = default!;

    public void OnGet()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        FavoritesList = _favoriteService.GetFilmFavoriteList(userId);
    }
}