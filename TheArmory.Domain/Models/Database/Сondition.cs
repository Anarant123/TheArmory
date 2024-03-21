using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

/// <summary>
/// Состояние оружия
/// </summary>
public class Сondition : DbEntity
{
    /// <summary>
    /// Название
    /// </summary>
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}


// идеальное
// хорошее
// среднее
// требует ремонта