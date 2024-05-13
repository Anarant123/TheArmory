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
    /// Id Состояния
    /// </summary>
    [JsonPropertyName("conditionId")]
    public WeaponCondition? ConditionId { get; set; }

    /// <summary>
    /// Id Категории
    /// </summary>
    [JsonPropertyName("CategoryId")]
    public Guid? CategoryId { get; set; }
}