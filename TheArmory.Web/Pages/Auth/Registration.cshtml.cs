using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Auth;

public class Registration : PageModel
{
    private readonly AuthService _service;
    
    [BindProperty]
    public UserCreateCommand Command { get; set; }
    
    public Registration(AuthService service)
    {
        _service = service;
    }
    
    public void OnGet()
    {
        
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _service.Registration(Command);
        if (result.Success)
        {
            return RedirectToPage("/Auth/Index");
        }

        ModelState.AddModelError(string.Empty, result.Error);
        return Page();
    }
}