using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

/// <summary>
/// Роль
/// </summary>
public class Role : DbEntity
{
    /// <summary>
    /// Название
    /// </summary>
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
}