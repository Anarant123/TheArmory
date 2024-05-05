using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace TheArmory.Domain.Models.Request.Commands.User;

public class UserAdminCreateCommand
{
    /// <summary>
    /// Логин пользователя
    /// </summary>
    [Required(ErrorMessage = "Введите логин")]
    [Display(Name = "Логин")]
    [DataType(DataType.Text)]
    [StringLength(128, ErrorMessage = "Логин должен быть не более 128 символов")]
    [JsonPropertyName("login")]
    public string? Login { set; get; }
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    [Required(ErrorMessage = "Введите имя")]
    [Display(Name = "Имя")]
    [DataType(DataType.Text)]
    [StringLength(128, ErrorMessage = "Имя должено быть не более 128 символов")]
    [JsonPropertyName("login")]
    public string? Name { set; get; }

    /// <summary>
    /// Пароль
    /// </summary>
    [Required(ErrorMessage = "Введите пароль")]
    [Display(Name = "Пароль")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-zа-яё])(?=.*[A-ZА-ЯЁ])(?=.*\d).{8,64}$",
        ErrorMessage = "Пароль должен содержать от 8 до 64 символов, включая как минимум одну заглавную букву, одну строчную букву и одну цифру")]
    [StringLength(64, MinimumLength = 8, ErrorMessage = "Пароль должен содержать от 8 до 64 символов")]
    [JsonPropertyName("password")]
    public string? Password { set; get; }

    /// <summary>
    /// Подтверждение пароля
    /// </summary>
    [Required(ErrorMessage = "Введите подтверждение пароля")]
    [DataType(DataType.Password)]
    [Display(Name = "Повторите пароль")]
    [JsonPropertyName("passwordConfirm")]
    [StringLength(64)]
    public string? PasswordConfirm { set; get; }
    
    [JsonIgnore] public IFormFile Photo { get; set; } 
}