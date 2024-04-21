using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Request.Commands.User;

public class UserCreateCommand
{
    [Required(ErrorMessage = "Введите имя(ФИО) пользователя")]
    [Display(Name = "Имя пользователя(ФИО)")]
    [RegularExpression(@"^([А-ЯЁA-Z][а-яёa-z]+[\-\s]?){1,3}$", ErrorMessage = "Некорректное имя пользователя")]
    [StringLength(50)]
    [JsonPropertyName("name")]
    public string? Name { set; get; }

    /// <summary>
    /// Логин пользователя
    /// </summary>
    [Required(ErrorMessage = "Введите логин")]
    [Display(Name = "Логин")]
    [DataType(DataType.Text)]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Некорректный формат логина")]
    [StringLength(128, ErrorMessage = "Логин должен быть не более 128 символов")]
    [JsonPropertyName("login")]
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

    /// <summary>
    /// Подтверждение пароля
    /// </summary>
    [Required(ErrorMessage = "Введите подтверждение пароля")]
    [DataType(DataType.Password)]
    [Display(Name = "Повторите пароль")]
    [JsonPropertyName("passwordConfirm")]
    [StringLength(64)]
    public string? PasswordConfirm { set; get; }
}