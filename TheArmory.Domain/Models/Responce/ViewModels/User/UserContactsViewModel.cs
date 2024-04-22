using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Database;

namespace TheArmory.Domain.Models.Responce.ViewModels.User;

public class UserContactsViewModel
{
    /// <summary>
    /// Название
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Фото профиля
    /// </summary>
    [JsonPropertyName("photoName")]
    public string? PhotoName { get; set; }
    
    /// <summary>
    /// Контакты
    /// </summary>
    [JsonPropertyName("contacts")]
    public List<Contact> Contacts { get; set; }
    
    public UserContactsViewModel(){}
    
    public UserContactsViewModel(Database.User user)
    {
        Name = user.Name;
        PhotoName = user.PhotoName is not null ? Path.Combine(user.Id.ToString(), "Profileinfo", user.PhotoName) : null;
        Contacts = user.Contacts;
    }
}