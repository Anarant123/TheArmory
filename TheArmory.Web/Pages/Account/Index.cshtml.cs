using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Responce.ViewModels.User;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Account;

public class Index : PageModel
{
    private readonly UserService _service;
    public UserViewModel CurrentUser { get; private set; }

    public Index(UserService service)
    {
        _service = service;
    }
    
    public void OnGet()
    {
        // Получаем текущего пользователя или другие необходимые данные
        //CurrentUser = _service.GetCurrentUser();
    }
}