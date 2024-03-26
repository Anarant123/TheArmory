using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Request.Commands.Ad;

public class AdCreateCommand
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
    public StateStatus StatusId { get; set; }
}