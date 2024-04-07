using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TheArmory.Web.Pages.Auth;

public class Index : PageModel
{
    // todo поменять на модель комманды
    [Required(ErrorMessage = "Поле 'Логин' обязательно для заполнения")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Поле 'Пароль' обязательно для заполнения")]
    public string Password { get; set; }
    
    public void OnGet()
    {
        
    }
}