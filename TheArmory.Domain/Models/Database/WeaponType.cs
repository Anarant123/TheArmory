using System.ComponentModel.DataAnnotations.Schema;

namespace TheArmory.Domain.Models.Database;

public class WeaponType : DbEntity
{
    /// <summary>
    /// Наименование
    /// </summary>
    [Column("name")]
    public string Name { get; set;}
    
    // virtual
    
    public virtual List<Characteristic> Characteristics { get; set; }
}