using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Responce.ViewModels.User;

public class UserViewModel
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
    /// Логин пользователя
    /// </summary>
    [JsonPropertyName("login")]
    public string Login { get; set; }

    /// <summary>
    /// Id Роли
    /// </summary>
    [JsonPropertyName("roleId")]
    public UserRole RoleId { get; set; }

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

    /// <summary>
    /// Дата последнего посещения
    /// </summary>
    [JsonPropertyName("lastVisitDate")]
    public DateTime LastVisitDate { get; set; }

    /// <summary>
    /// Фото профиля
    /// </summary>
    [JsonPropertyName("photoName")]
    public string? PhotoName { get; set; }

    [JsonIgnore] public string BaseUrl { get; set; } = "";

    public UserViewModel()
    {
    }

    public UserViewModel(Database.User user)
    {
        Id = user.Id;
        Name = user.Name;
        RoleId = user.RoleId;
        StatusId = user.StatusId;
        RegistrationDateTime = user.RegistrationDateTime;
        LastVisitDate = user.LastVisitDate;
        PhotoName = user.PhotoName is not null ? Path.Combine(user.Id.ToString(), "Profileinfo", user.PhotoName) : null;
    }
}