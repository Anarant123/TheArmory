using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Responce.ViewModels.User;

public class UserPersonalInfoViewModel
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
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
    /// Id региона
    /// </summary>
    [JsonPropertyName("region")]
    public Database.Region Region { get; set; }

    /// <summary>
    /// Id статуса
    /// </summary>
    [JsonPropertyName("status")]
    public Status Status { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    [JsonPropertyName("registrationDateTime")]
    public DateTime RegistrationDateTime { get; set; }
    
    /// <summary>
    /// Контакты пользователя
    /// </summary>
    [JsonPropertyName("contacts")]
    public List<Contact> Contacts { get; set; } = new List<Contact>();
    
    public UserPersonalInfoViewModel(){}

    public UserPersonalInfoViewModel(Database.User user)
    {
        Id = user.Id;
        Name = user.Name;
        PhotoName = user.PhotoName is not null ? Path.Combine(user.Id.ToString(), "Profileinfo", user.PhotoName) : null;
        Region = user.Region;
        Status = user.Status;
        RegistrationDateTime = user.RegistrationDateTime;
        Contacts = user.Contacts;
    }
}