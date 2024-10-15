using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group06_Project.Web.Areas.Admin.Pages.User;

public class IndexModel : PageModel
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IUserService _userService;

    public IndexModel(IUserService userService, UserManager<Domain.Entities.User> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }

    [BindProperty(SupportsGet = true)] public string? Search { get; set; }
    [BindProperty(SupportsGet = true)] public int? PageNo { get; set; }
    public Page<UserList> Users { get; set; } = null!;

    public void OnGet()
    {
        Users = _userService.GetUserList(Search, PageNo);
    }

    public async Task<IActionResult> OnPostDisableUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        await _userManager.SetLockoutEnabledAsync(user, true);
        await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
        // if (success)
        //     TempData["Message"] = "Khóa tài khoản người dùng thành công.";
        // else
        //     TempData["Error"] = "Vô hiệu hóa khóa tài khoản người dùng thất bại.";
        TempData["Message"] = "Khóa tài khoản người dùng thành công.";
        return RedirectToPage(new { Search, PageNo });
    }

    public async Task<IActionResult> OnPostEnableUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        await _userManager.SetLockoutEnabledAsync(user, false);
        await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
        TempData["Message"] = "Mở Khóa tài khoản người dùng thành công.";
        return RedirectToPage(new { Search, PageNo });
    }
}