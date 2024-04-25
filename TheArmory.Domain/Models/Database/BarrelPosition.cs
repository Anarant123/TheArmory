using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

public class BarrelPosition : DbEntity
{
    /// <summary>
    /// Наименование
    /// </summary>
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set;}
    
    // virtual
    
    public virtual List<Characteristic> Characteristics { get; set; }
    
    public BarrelPosition(){}

    public BarrelPosition(string name)
    {
        Name = name;
    }
}