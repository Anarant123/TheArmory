using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Request.Commands.User;

public class UserChangePasswordCommand : UserCommand
{
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