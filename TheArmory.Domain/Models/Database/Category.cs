using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

public class Category : DbEntity
{
    /// <summary>
    /// Наименование
    /// </summary>
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set;}
    
    // virtual
    
    public virtual List<Ad> Ads { get; set; }
    
    public Category(){}

    public Category(string name)
    {
        Name = name;
    }
    
}