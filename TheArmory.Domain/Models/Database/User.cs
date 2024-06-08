using System.ComponentModel.DataAnnotations.Schema;
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
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Логин пользователя
    /// </summary>
    [Column("login")]
    public string Login { get; set; }

    /// <summary>
    /// Хэш пароля
    /// </summary>
    [Column("passwordHash")]
    public string PasswordHash { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    [Column("registrationDateTime")]
    public DateTime RegistrationDateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// Дата последнего посещения
    /// </summary>
    [Column("lastVisitDate")]
    public DateTime LastVisitDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Фото профиля
    /// </summary>
    [Column("photoName")]
    public string? PhotoName { get; set; }

    // foreign key

    /// <summary>
    /// Id региона
    /// </summary>
    [Column("regionId")]
    public Guid? RegionId { get; set; }

    /// <summary>
    /// Id Роли
    /// </summary>
    [Column("roleId")]
    public UserRole RoleId { get; set; }

    /// <summary>
    /// Id статуса
    /// </summary>
    [Column("statusId")]
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

    /// <summary>
    /// Список контактов
    /// </summary>
    public virtual List<Contact> Contacts { get; set; }
}