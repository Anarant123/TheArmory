using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Request.Commands.Ad;

public class AdUpdateCommand : AdCommand
{
    /// <summary>
    /// Наименование
    /// </summary>
    [JsonPropertyName("name")]
    [AllowNull]
    public string? Name { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    [JsonPropertyName("description")]
    [AllowNull]
    public string? Description { get; set; }

    /// <summary>
    /// Ссылка на YouTube видео с обзором
    /// </summary>
    [JsonPropertyName("youtubeLink")]
    [AllowNull]
    public string? YouTubeLink { get; set; }

    /// <summary>
    /// Id Состояния
    /// </summary>
    [JsonPropertyName("conditionId")]
    public WeaponCondition? ConditionId { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    [JsonPropertyName("address")]
    [AllowNull]
    public string? Address { get; set; }

    /// <summary>
    /// Широта
    /// </summary>
    [JsonPropertyName("latitude")]
    [AllowNull]
    public string? Latitude { get; set; }

    /// <summary>
    /// Долгота
    /// </summary>
    [JsonPropertyName("longitude")]
    [AllowNull]
    public string? Longitude { get; set; }

    /// <summary>
    /// Id Категории
    /// </summary>
    [JsonPropertyName("CategoryId")]
    public Guid? CategoryId { get; set; }

    [JsonPropertyName("caliberId")]
    public Guid? CaliberId { get; set; }

    [JsonPropertyName("weaponTypeId")]
    public Guid? WeaponTypeId { get; set; }

    [JsonPropertyName("barrelPositionId")]
    public Guid? BarrelPositionId { get; set; }

    [JsonPropertyName("yearOfProduction")]
    public int? YearOfProduction { get; set; }
}