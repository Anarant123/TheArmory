using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.User;

namespace TheArmory.Web.Pages.Auth;

public class Registration : PageModel
{
    public UserCreateCommand Command;
    public void OnGet()
    {
        
    }
}