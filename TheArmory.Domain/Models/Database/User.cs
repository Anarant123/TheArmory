using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

/// <summary>
/// Пользователь
/// </summary>
public class User : DbEntity
{
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
     
    // foreign key
    // todo заполнить на основе виртуальных свойств ниже
    
    // virtual 
    
    /// <summary>
    /// Регион по умолчанию
    /// </summary>
    public virtual Region Region { get; set; }
    
    /// <summary>
    /// Список объявлений
    /// </summary>
    public virtual List<Ad> Ads { get; set; }
}