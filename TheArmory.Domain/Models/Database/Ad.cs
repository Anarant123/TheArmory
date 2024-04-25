using System.ComponentModel.DataAnnotations.Schema;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Database;

/// <summary>
/// Объявление
/// </summary>
public class Ad : DbEntity
{
    /// <summary>
    /// Наименование
    /// </summary>
    [Column("name")]
    public string Name { get; set;}
    
    /// <summary>
    /// Цена
    /// </summary>
    [Column("price")]
    public decimal Price { get; set; }
    
    /// <summary>
    /// Старая цена
    /// </summary>
    [Column("oldPrice")]
    public decimal? OldPrice { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    [Column("description")]
    public string? Description { get; set; } = string.Empty;

    /// <summary>
    /// Дата создания
    /// </summary>
    [Column("creationDateTime")]
    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// Количество просмотров
    /// </summary>
    [Column("countOfViews")]
    public int CountOfViews { get; set; } = 0;

    /// <summary>
    /// Количество просмотров за сегодня
    /// </summary>
    [Column("countOfViewsToday")]
    public int CountOfViewsToday { get; set; } = 0;

    /// <summary>
    /// Дата последнего посещения
    /// </summary>
    [Column("lastVisitDate")]
    public DateTime LastVisitDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Ссылка на ютуб видео с обзором
    /// </summary>
    [Column("youTubeLink")]
    public string? YouTubeLink { get; set; } = string.Empty;
    
    //todo поле координатов
    
    // foreign key
    
    /// <summary>
    /// Id Состояния
    /// </summary>
    [Column("conditionId")]
    public WeaponCondition ConditionId { get; set; }
    
    /// <summary>
    /// Id Региона
    /// </summary>
    [Column("regionId")]
    public Guid? RegionId { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    [Column("userId")]
    public Guid UserId { get; set; }

    /// <summary>
    /// Id статуса
    /// </summary>
    [Column("statusId")]
    public StateStatus StatusId { get; set; } = StateStatus.Actively;
    
    /// <summary>
    /// Id категории
    /// </summary>
    [Column("categoryId")]
    public Guid CategoryId { get; set; }
    
    /// <summary>
    /// Id характеристики
    /// </summary>
    [Column("characteristicId")]
    public Guid? CharacteristicId { get; set; }
    
    // virtual 
    
    public virtual Condition Condition { get; set; }
    
    public virtual Region? Region { get; set; }
    
    public virtual User User { get; set; }
    
    public virtual Category Category { get; set; }
    
    public virtual Characteristic? Characteristic { get; set; }
    
    public virtual Status Status { get; set; }
    
    public virtual List<Complaint> Complaints { get; set; }
    
    public virtual List<Favorite> Favorites { get; set; }
    
    public virtual List<Media> Medias { get; set; }
    
    public virtual Location Location { get; set; }
}