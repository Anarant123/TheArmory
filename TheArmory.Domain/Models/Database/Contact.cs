using System.ComponentModel.DataAnnotations.Schema;

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
    
    public virtual User User { get; set; }
}