using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Database;

/// <summary>
/// Пользователь
/// </summary>
public class User : DbEntity
{
    /// <summary>
    /// Название
    /// </summary>
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
    /// Хэш пароля
    /// </summary>
    public string PasswordHash { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime RegistrationDateTime { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Дата последнего посещения
    /// </summary>
    public DateTime LastVisitDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Фото профиля
    /// </summary>
    public string? PhotoName { get; set; }
     
    // foreign key
    // todo заполнить на основе виртуальных свойств ниже
    
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

    // virtual 
    
    /// <summary>
    /// Регион по умолчанию
    /// </summary>
    public virtual Region Region { get; set; }
    
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public virtual Role Role { get; set; }
    
    /// <summary>
    /// Статус пользователя
    /// </summary>
    public virtual Status Status { get; set; }
    
    /// <summary>
    /// Список объявлений
    /// </summary>
    public virtual List<Ad> Ads { get; set; }
    
    /// <summary>
    /// Жалобы пользователя
    /// </summary>
    public virtual List<Complaint> Complaints { get; set; }
    
    /// <summary>
    /// Избранное пользователя
    /// </summary>
    public virtual List<Favorite> Favorites { get; set; }
    
}