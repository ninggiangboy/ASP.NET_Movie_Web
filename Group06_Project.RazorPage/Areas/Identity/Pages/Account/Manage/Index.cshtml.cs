// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Enums;
using Group06_Project.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group06_Project.RazorPage.Areas.Identity.Pages.Account.Manage;

public class IndexModel : PageModel
{
    private readonly IBalanceService _balanceService;
    private readonly ILogger<IndexModel> _logger;
    private readonly IPaymentService _paymentService;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public IndexModel(
        UserManager<User> userManager,
        SignInManager<User> signInManager, IPaymentService paymentService, ILogger<IndexModel> logger,
        IBalanceService balanceService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _paymentService = paymentService;
        _logger = logger;
        _balanceService = balanceService;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string Username { get; set; }

    public decimal Balance { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string StatusMessage { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    [BindProperty] public InputTopUpModel InputTopUp { get; set; }

    private Task LoadAsync(User user)
    {
        // var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

        Username = user.Email;
        Balance = user.Balance;
        return Task.CompletedTask;
        //
        // Input = new InputModel
        // {
        //     PhoneNumber = phoneNumber
        // };
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        await LoadAsync(user);
        return Page();
    }

    // public async Task<IActionResult> OnPostAsync()
    // {
    //     var user = await _userManager.GetUserAsync(User);
    //     if (user == null) return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
    //
    //     if (!ModelState.IsValid)
    //     {
    //         await LoadAsync(user);
    //         return Page();
    //     }
    //
    //     var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
    //     if (Input.PhoneNumber != phoneNumber)
    //     {
    //         var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
    //         if (!setPhoneResult.Succeeded)
    //         {
    //             StatusMessage = "Unexpected error when trying to set phone number.";
    //             return RedirectToPage();
    //         }
    //     }
    //
    //     await _signInManager.RefreshSignInAsync(user);
    //     StatusMessage = "Your profile has been updated";
    //     return RedirectToPage();
    // }

    public async Task<IActionResult> OnPostTopUpAsync(string returnUrl = null)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        _logger.LogInformation("User {userId} top up {amount}", userId, InputTopUp.Amount);
        var trans = await _balanceService.CreateTransactionAsync(InputTopUp.Amount, TransactionType.TopUp, userId);
        var url = await _paymentService.CreatePaymentUrl(trans);
        return Redirect(url);
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InputModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }

    public class InputTopUpModel
    {
        [Required] public decimal Amount { get; set; }
    }
}