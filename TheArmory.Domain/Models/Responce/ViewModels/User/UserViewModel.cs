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
    /// Id региона
    /// </summary>
    [JsonPropertyName("regionId")]
    public Guid? RegionId { get; set; }
    
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

    public UserViewModel(){}
    
    public UserViewModel(Database.User user)
    {
        Id = user.Id;
        Name = user.Name;
        RegionId = user.RegionId;
        RoleId = user.RoleId;
        StatusId = user.StatusId;
    }
    
}