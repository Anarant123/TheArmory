using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Web.Service;
using TheArmory.Web.Utils;

namespace TheArmory.Web.Pages.Auth;

public class Index : PageModel
{
    private readonly AuthService _service;
    
    [BindProperty] public BaseResult Result { get; set; } = new BaseResult();
    
    [BindProperty]
    public UserLoginCommand Command { get; set; }

    public Index(AuthService service)
    {
        _service = service;
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        if (User.Identity is {IsAuthenticated: true })
            return RedirectToPage("/Account/PersonalInfo");
        
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        var result = await _service.Login(Command);
        Result = result;
        if (result.Success)
        {
            await AuthUtils.SetLoginClaims(result.Item, HttpContext, Command?.RememberMe == true);
            return RedirectToPage("/Account/PersonalInfo");
        }

        ModelState.AddModelError(string.Empty, result.Error);
        return Page();
    }
}