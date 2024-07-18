using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group06_Project.RazorPage.Areas.Admin.Pages.User;

public class IndexModel : PageModel
{
    private readonly IUserService _userService;

    public IndexModel(IUserService userService)
    {
        _userService = userService;
    }

    [BindProperty(SupportsGet = true)] public string? Search { get; set; }
    [BindProperty(SupportsGet = true)] public int? PageNo { get; set; }
    public Page<UserList> Users { get; set; } = null!;

    public void OnGet()
    {
        Users = _userService.GetUserList(Search, PageNo);
    }

    public IActionResult OnPostDisableUser(string userId)
    {
        Console.WriteLine($"UserId received: {userId}");
        var success = _userService.DisableUserLockout(userId);
        if (success)
            TempData["Message"] = "Khóa tài khoản người dùng thành công.";
        else
            TempData["Error"] = "Vô hiệu hóa khóa tài khoản người dùng thất bại.";
        return RedirectToPage(new { Search, PageNo });
    }

    public IActionResult OnPostEnableUser(string userId)
    {
        Console.WriteLine($"UserId received: {userId}");
        var success = _userService.EnableUserLockout(userId);
        if (success)
            TempData["Message"] = "Mở khóa tài khoản người dùng đã được vô hiệu hóa thành công.";
        else
            TempData["Error"] = "Vô hiệu hóa khóa tài khoản người dùng thất bại.";
        return RedirectToPage(new { Search, PageNo });
    }
}