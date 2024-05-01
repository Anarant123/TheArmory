using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Request.Commands.Ad;

public class AdUpdateCommand
{
    /// <summary>
    /// Наименование
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; } = string.Empty;

    /// <summary>
    /// Ссылка на YouTube видео с обзором
    /// </summary>
    [JsonPropertyName("youtubeLink")]
    public string? YouTubeLink { get; set; } = string.Empty;

    /// <summary>
    /// Id Состояния
    /// </summary>
    [JsonPropertyName("conditionId")]
    public WeaponCondition ConditionId { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; } = string.Empty;

    /// <summary>
    /// Широта
    /// </summary>
    [JsonPropertyName("latitude")]
    public string? Latitude { get; set; } = string.Empty;

    /// <summary>
    /// Долгота
    /// </summary>
    [JsonPropertyName("longitude")]
    public string? Longitude { get; set; } = string.Empty;

    /// <summary>
    /// Id Категории
    /// </summary>
    [JsonPropertyName("CategoryId")]
    public Guid CategoryId { get; set; } = Guid.Empty;

    [JsonPropertyName("caliberId")] 
    public Guid? CaliberId { get; set; } = Guid.Empty;

    [JsonPropertyName("weaponTypeId")] 
    public Guid? WeaponTypeId { get; set; } = Guid.Empty;

    [JsonPropertyName("barrelPositionId")] 
    public Guid? BarrelPositionId { get; set; } = Guid.Empty;

    [JsonPropertyName("yearOfProduction")] 
    public int? YearOfProduction { get; set; } = 0;
}