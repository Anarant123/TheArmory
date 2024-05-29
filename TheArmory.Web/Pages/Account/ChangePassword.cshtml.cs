using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Account;

public class ChangePassword : PageModel
{
    private readonly UserService _userService;
    
    [BindProperty] public UserChangePasswordCommand ChangePasswordCommand { get; set; }
    [BindProperty] public BaseResult RequestResult { get; set; } = new BaseResult();

    public ChangePassword(UserService userService)
    {
        _userService = userService;
    }
    
    public async Task<ActionResult> OnGetAsync()
    {
        return Page();
    }
    
    public async Task<ActionResult> OnPostDeleteAsync()
    {
        RequestResult = await _userService.ChangePassword(ChangePasswordCommand);
        if (!RequestResult.Success) return await OnGetAsync();
        return RedirectToPage("/Auth/Index");
    }
}