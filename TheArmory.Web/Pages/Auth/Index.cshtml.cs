using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Auth;

public class Index : PageModel
{
    private readonly AuthService _service;
    
    [BindProperty]
    public UserLoginCommand Command { get; set; }

    public Index(AuthService service)
    {
        _service = service;
    }
    
    public void OnGet()
    {
        
    }
    
    public async Task<IActionResult> OnPostAsync(UserLoginCommand command)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _service.Login(Command);
        if (result?.Item != null)
        {
            return RedirectToPage("/Account/Index");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
            return Page();
        }
    }
}