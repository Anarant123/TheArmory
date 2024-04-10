using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Responce.ViewModels.User;

namespace TheArmory.Web.Pages.Account;

public class PersonalInfo : PageModel
{
    [BindProperty]
    public UserPersonalInfoViewModel User { get; set; }
    
    public void OnGet()
    {
        
    }
}