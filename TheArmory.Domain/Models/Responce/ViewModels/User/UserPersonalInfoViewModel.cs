using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
    
    /// <summary>
    /// Id региона
    /// </summary>
    [JsonPropertyName("regionId")]
    public Guid? RegionId { get; set; }

    /// <summary>
    /// Id статуса
    /// </summary>
    [JsonPropertyName("statusId")]
    public StateStatus StatusId { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    [JsonPropertyName("registrationDateTime")]
    public DateTime RegistrationDateTime { get; set; }
    
    public UserPersonalInfoViewModel(){}

    public UserPersonalInfoViewModel(Database.User user)
    {
        Id = user.Id;
        Name = user.Name;
        PhoneNumber = user.PhoneNumber;
        Email = user.Email;
        RegionId = user.RegionId;
        StatusId = user.StatusId;
        RegistrationDateTime = user.RegistrationDateTime;
    }
}