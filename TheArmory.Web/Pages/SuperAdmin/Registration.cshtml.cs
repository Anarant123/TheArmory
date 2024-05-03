using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.SuperAdmin;

public class Registration : PageModel
{
    private readonly AdminsService _service;
    
    [BindProperty] public BaseResult RequestResult { get; set; } = new BaseResult();
    
    [BindProperty]
    public UserAdminCreateCommand Command { get; set; }
    
    public Registration(AdminsService service)
    {
        _service = service;
    }
    
    public async Task<IActionResult> OnGetAsync()
    {
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Error");
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = await _service.Registration(Command);
        RequestResult = result;
        if (result.Success)
        {
            return RedirectToPage("/SuperAdmin/Index");
        }

        ModelState.AddModelError(string.Empty, result.Error);
        return Page();
    }
}