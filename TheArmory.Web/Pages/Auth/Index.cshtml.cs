using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Web.Service;
using TheArmory.Web.Utils;

namespace TheArmory.Web.Pages.Auth;

public class Index : PageModel
{
    private readonly AuthService _service;

    [BindProperty] public BaseResult RequestResult { get; set; } = new BaseResult();
    [BindProperty] public UserLoginCommand Command { get; set; }

    public Index(AuthService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        if (User.Identity is { IsAuthenticated: true })
            return RedirectToPage("/Account/PersonalInfo");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Error");
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _service.Login(Command);
        RequestResult = result;
        if (result.Success)
        {
            await AuthUtils.SetLoginClaims(result.Item, HttpContext, Command?.RememberMe == true);
            return result.Item.RoleId switch
            {
                UserRole.SuperAdmin => RedirectToPage("/SuperAdmin/Index"),
                UserRole.Admin => RedirectToPage("/Admin/Index"),
                UserRole.Client => RedirectToPage("/Account/PersonalInfo"),
                _ => Page()
            };
        }

        ModelState.AddModelError(string.Empty, result.Error);
        return Page();
    }
}