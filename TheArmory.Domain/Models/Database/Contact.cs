using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

public class Contact : DbEntity
{
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }
   
    [Column("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [Column("userId")]
    [JsonIgnore]
    public Guid UserId { get; set; }
    
    // virtual
    
    [JsonIgnore]
    public virtual User User { get; set; }
}