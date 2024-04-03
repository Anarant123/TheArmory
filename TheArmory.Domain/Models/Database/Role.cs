using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Database;

/// <summary>
/// Роль
/// </summary>
public class Role
{
    [Key]
    [Column("id")]
    [JsonPropertyName("id")]
    public UserRole Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    // virtual
    
    public virtual List<User> Users { get; set; }

    public Role(UserRole id, string name)
    {
        Id = id;
        Name = name;
    }
}