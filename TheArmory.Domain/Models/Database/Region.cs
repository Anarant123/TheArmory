using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

/// <summary>
/// Регион РФ
/// </summary>
public class Region : DbEntity
{
    /// <summary>
    /// Название
    /// </summary>
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Код региона
    /// </summary>
    [Column("code")]
    [JsonPropertyName("code")]
    public int Code { get; set; }
}