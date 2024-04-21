using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Request.Commands.User;

public class UserLoginCommand
{
    /// <summary>
    /// Логин (может быть электронная почта или номер телефона)
    /// </summary>
    [Required(ErrorMessage = "Введите логин")]
    [Display(Name = "Login")]
    [StringLength(128)]
    [JsonPropertyName("login")]
    public string? Login { set; get; }

    /// <summary>
    /// Пароль
    /// </summary>
    [Required(ErrorMessage = "Введите пароль")]
    [Display(Name = "Пароль")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^\S+$", ErrorMessage = "Пароль не должен содержать пробелы")]
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    /// <summary>
    /// Сохранить сессию
    /// </summary>
    [JsonPropertyName("rememberMe")]
    public bool RememberMe { get; set; } = true;
}