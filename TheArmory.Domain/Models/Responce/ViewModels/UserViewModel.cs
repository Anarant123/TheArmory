using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Responce.ViewModels;

public class UserViewModel
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;
    
    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; }
    
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

    public UserViewModel(User user)
    {
        Id = user.Id;
        Name = user.Name;
        PhoneNumber = user.PhoneNumber;
        Email = user.Email;
        RegionId = user.RegionId;
        RoleId = user.RoleId;
        StatusId = user.StatusId;
    }
    
}