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
    /// Номер телефона
    /// </summary>
    [Display(Name = "Номер телефона")]
    [RegularExpression(@"^(?:\+7\d{10})?$", ErrorMessage = "Введите корректный номер телефона или оставьте поле пустым")]
    [StringLength(12)]
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { set; get; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    [Required(ErrorMessage = "Введите электронную почту")]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^\S+@\S+\.\S+$", ErrorMessage = "Некорректная почта")]
    [JsonPropertyName("email")]
    [StringLength(128)]
    public string? Email { set; get; }

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