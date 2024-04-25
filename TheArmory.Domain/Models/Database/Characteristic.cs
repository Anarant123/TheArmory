using System.ComponentModel.DataAnnotations.Schema;

namespace TheArmory.Domain.Models.Database;

public class Characteristic : DbEntity
{
    [Column("caliberId")]
    public Guid CaliberId { get; set; }
    
    [Column("weaponTypeId")]
    public Guid WeaponTypeId { get; set; }
    
    [Column("barrelPositionId")]
    public Guid BarrelPositionId { get; set; }
    
    [Column("yearOfProduction")]
    public int YearOfProduction  { get; set; }
    
    // virtual
    public virtual WeaponType WeaponType { get; set; }
    public virtual BarrelPosition BarrelPosition { get; set; }
    public virtual Caliber Caliber { get; set; }
    public virtual List<Ad> Ads { get; set; }
}