using System.ComponentModel.DataAnnotations.Schema;

namespace TheArmory.Domain.Models.Database;

/// <summary>
/// Избранное объявление
/// </summary>
public class Favorite : DbEntity
{
    // foreign key

    /// <summary>
    /// Объявление 
    /// </summary>
    [Column("adId")]
    public Guid AdId { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    [Column("userId")]
    public Guid UserId { get; set; }

    // virtual 
    public virtual Ad Ad { get; set; }

    public virtual User User { get; set; }
}