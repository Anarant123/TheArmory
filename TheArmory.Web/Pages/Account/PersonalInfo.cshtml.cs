using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Responce.ViewModels.User;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Account;

public class PersonalInfo : PageModel
{
    private readonly UserService _userService;
    public readonly string BaseUrl;
    
    [BindProperty]
    public UserPersonalInfoViewModel User { get; set; }


    public PersonalInfo(UserService userService, BaseUrlOptions baseUrlOptions)
    {
        _userService = userService;
        BaseUrl = baseUrlOptions.GetFullApiUrl("Files");
    }
    
    public async Task OnGetAsync()
    {
        var userResponce = await _userService.GetMe();
        User = userResponce.Item;
    }
}