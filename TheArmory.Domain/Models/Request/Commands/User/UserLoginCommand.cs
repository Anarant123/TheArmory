using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Request.Commands.User;

public class UserLoginCommand
{
    /// <summary>
    /// Логин (может быть электронная почта или номер телефона)
    /// </summary>
    [Required(ErrorMessage = "Введите логин (электронную почту или номер телефона)")]
    [Display(Name = "Login")]
    [JsonPropertyName("login")]
    [RegularExpression(@"^(?:\+7\d{10})?$|^\S+@\S+\.\S+$", ErrorMessage = "Введите корректный логин (электронную почту или номер телефона)")]
    [StringLength(128)]
    public string? Login { set; get; }

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
}