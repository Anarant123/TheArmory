using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Responce.ViewModels.User;

public class UserViewModel
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Логин пользователя
    /// </summary>
    [JsonPropertyName("Login")]
    public string Login { get; set; }
    
    /// <summary>
    /// Id региона
    /// </summary>
    public Guid? RegionId { get; set; }
    
    /// <summary>
    /// Id Роли
    /// </summary>
    public UserRole RoleId { get; set; }

    /// <summary>
    /// Id статуса
    /// </summary>
    public StateStatus StatusId { get; set; }

    public UserViewModel(Database.User user)
    {
        Id = user.Id;
        Name = user.Name;
        RegionId = user.RegionId;
        RoleId = user.RoleId;
        StatusId = user.StatusId;
    }
    
}