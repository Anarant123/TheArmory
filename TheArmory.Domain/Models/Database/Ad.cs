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
    public decimal OldPrice { get; set; }

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
    public int CountOfViews { get; set; }
    
    /// <summary>
    /// Количество просмотров за сегодня
    /// </summary>
    public int CountOfViewsToday { get; set; }

    /// <summary>
    /// Дата последнего посещения
    /// </summary>
    public DateTime LastVisitDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Ссылка на ютуб видео с обзором
    /// </summary>
    public string? YouToubeLink { get; set; } = string.Empty;
    
    //todo поле координатов
    
    // foreign key
    // todo заполнить на основе виртуальных свойств ниже
    
    /// <summary>
    /// Id Состояния
    /// </summary>
    public Guid ConditionId { get; set; }
    
    /// <summary>
    /// Id Региона
    /// </summary>
    public Guid RegionId { get; set; }
    
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    // virtual 
    
    /// <summary>
    /// Состояние
    /// </summary>
    public virtual Сondition Condition { get; set; }
    
    /// <summary>
    /// Регион продажи
    /// </summary>
    public virtual Region Region { get; set; }
    
    /// <summary>
    /// Пользователь, разместивший объявление
    /// </summary>
    public virtual User User { get; set; }
}