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
    public string Name { get; set;}
    
    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Старая цена
    /// </summary>
    public decimal? OldPrice { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; } = string.Empty;

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreationDateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// Количество просмотров
    /// </summary>
    public int CountOfViews { get; set; } = 0;

    /// <summary>
    /// Количество просмотров за сегодня
    /// </summary>
    public int CountOfViewsToday { get; set; } = 0;

    /// <summary>
    /// Дата последнего посещения
    /// </summary>
    public DateTime LastVisitDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Ссылка на ютуб видео с обзором
    /// </summary>
    public string? YouTubeLink { get; set; } = string.Empty;
    
    //todo поле координатов
    
    // foreign key
    
    /// <summary>
    /// Id Состояния
    /// </summary>
    public WeaponCondition ConditionId { get; set; }
    
    /// <summary>
    /// Id Региона
    /// </summary>
    public Guid RegionId { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Id статуса
    /// </summary>
    public StateStatus StatusId { get; set; } = StateStatus.Actively;
    
    // virtual 
    
    /// <summary>
    /// Состояние
    /// </summary>
    public virtual Condition Condition { get; set; }
    
    /// <summary>
    /// Регион продажи
    /// </summary>
    public virtual Region Region { get; set; }
    
    /// <summary>
    /// Пользователь, разместивший объявление
    /// </summary>
    public virtual User User { get; set; }
    
    /// <summary>
    /// Статус объявления
    /// </summary>
    public virtual Status Status { get; set; }
    
    public virtual List<Complaint> Complaints { get; set; }
    
    public virtual List<Favorite> Favorites { get; set; }
    
    public virtual List<Media> Medias { get; set; }
}