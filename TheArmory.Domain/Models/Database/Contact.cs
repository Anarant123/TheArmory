using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

public class Contact : DbEntity
{
    [Column("name")]
    public string Name { get; set; }
   
    [Column("description")]
    public string Description { get; set; }
    
    [Column("userId")]
    public Guid UserId { get; set; }
    
    // virtual
    
    [JsonIgnore]
    public virtual User User { get; set; }
}