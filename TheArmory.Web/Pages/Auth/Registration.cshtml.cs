using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Auth;

public class Registration : PageModel
{
    private readonly AuthService _service;
    
    [BindProperty] public BaseResult Result { get; set; } = new BaseResult();
    
    [BindProperty]
    public UserCreateCommand Command { get; set; }
    
    public Registration(AuthService service)
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
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _service.Registration(Command);
        Result = result;
        if (result.Success)
        {
            return RedirectToPage("/Auth/Index");
        }

        ModelState.AddModelError(string.Empty, result.Error);
        return Page();
    }
}