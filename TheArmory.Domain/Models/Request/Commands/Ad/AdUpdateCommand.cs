using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Request.Commands.Ad;

public class AdUpdateCommand
{
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string? Name { get; set; } = string.Empty;
        
    /// <summary>
    /// Цена
    /// </summary>
    public decimal? Price { get; set; } 

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; } = string.Empty;
        
    /// <summary>
    /// Ссылка на ютуб видео с обзором
    /// </summary>
    public string? YouToubeLink { get; set; } = string.Empty;
        
    /// <summary>
    /// Id Состояния
    /// </summary>
    public WeaponCondition? ConditionId { get; set; }
        
    /// <summary>
    /// Id Региона
    /// </summary>
    public Guid? RegionId { get; set; }
}