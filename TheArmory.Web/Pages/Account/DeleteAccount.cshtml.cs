using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Account;

public class DeleteAccount : PageModel
{
    private readonly UserService _userService;
    [BindProperty] public BaseResult Result { get; set; } = new BaseResult();

    public DeleteAccount(UserService userService)
    {
        _userService = userService;
    }

    public async Task<ActionResult> OnGetAsync()
    {
        return Page();
    }

    public async Task<ActionResult> OnPostDeleteAsync()
    {
        Result = await _userService.DeleteMe();
        if (!Result.Success) return await OnGetAsync();
        await HttpContext.SignOutAsync();
        return RedirectToPage("/Auth/Index");
    }
}