using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Request.Commands.Ad;

public class AdLocationCommand : AdCommand
{
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
}