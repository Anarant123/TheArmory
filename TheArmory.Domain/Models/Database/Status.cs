using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Database;

public class Status
{
    [Key]
    [Column("id")]
    [JsonPropertyName("id")]
    public StateStatus Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    // virtual
    
    public virtual List<User> Users { get; set; }
    
    public virtual List<Ad> Ads { get; set; }

    public Status(StateStatus id, string name)
    {
        Id = id;
        Name = name;
    }
}