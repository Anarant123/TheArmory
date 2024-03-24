using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Database;

/// <summary>
/// Состояние оружия
/// </summary>
public class Condition : DbEntity
{
    [Key]
    [Column("id")]
    [JsonPropertyName("id")]
    public static WeaponCondition Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    // virtual
    public virtual List<Ad> Ads { get; set; }
}