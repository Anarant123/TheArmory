using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Responce.ViewModels;

public class AdViewModel
{
    public Guid Id { get; set; }
    
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
    public string? Description { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreationDateTime { get; set; }

    /// <summary>
    /// Количество просмотров
    /// </summary>
    public int CountOfViews { get; set; } = 0;

    /// <summary>
    /// Количество просмотров за сегодня
    /// </summary>
    public int CountOfViewsToday { get; set; }

    /// <summary>
    /// Дата последнего посещения
    /// </summary>
    public DateTime LastVisitDate { get; set; }
    
    /// <summary>
    /// Ссылка на ютуб видео с обзором
    /// </summary>
    public string? YouToubeLink { get; set; }
    
    /// <summary>
    /// Id Состояния
    /// </summary>
    public WeaponCondition ConditionId { get; set; }
    
    /// <summary>
    /// Id Региона
    /// </summary>
    public Guid RegionId { get; set; }

    public AdViewModel(Ad ad)
    {
        Id = ad.Id;
        Name = ad.Name;
        Price = ad.Price;
        OldPrice = ad.OldPrice;
        Description = ad.Description;
        CreationDateTime = ad.CreationDateTime;
        CountOfViews = ad.CountOfViews;
        CountOfViewsToday = ad.CountOfViewsToday;
        LastVisitDate = ad.LastVisitDate;
        YouToubeLink = ad.YouToubeLink;
        ConditionId = ad.ConditionId;
        RegionId = ad.RegionId;
    }
}