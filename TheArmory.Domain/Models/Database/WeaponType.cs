using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

public class WeaponType : DbEntity
{
    /// <summary>
    /// Наименование
    /// </summary>
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set;}
    
    // virtual
    
    public virtual List<Characteristic> Characteristics { get; set; }
    
    public WeaponType() {}

    public WeaponType(string name)
    {
        Name = name;
    }
}