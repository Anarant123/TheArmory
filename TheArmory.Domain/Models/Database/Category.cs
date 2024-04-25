using System.ComponentModel.DataAnnotations.Schema;

namespace TheArmory.Domain.Models.Database;

public class Category : DbEntity
{
    /// <summary>
    /// Наименование
    /// </summary>
    [Column("name")]
    public string Name { get; set;}
    
    // virtual
    
    public virtual List<Ad> Ads { get; set; }
    
}