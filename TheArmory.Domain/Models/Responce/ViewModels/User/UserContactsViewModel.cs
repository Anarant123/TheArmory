using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Responce.ViewModels.User;

public class UserContactsViewModel
{
    /// <summary>
    /// Название
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Номер телефона
    /// </summary>
    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Электронная почта
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    /// <summary>
    /// Фото профиля
    /// </summary>
    [JsonPropertyName("photoName")]
    public string? PhotoName { get; set; }
    
    public UserContactsViewModel(Database.User user)
    {
        Name = user.Name;
        PhoneNumber = user.PhoneNumber;
        Email = user.Email;
        PhotoName = user.PhotoName is not null ? Path.Combine(user.Id.ToString(), "Profileinfo", user.PhotoName) : null;
    }
}