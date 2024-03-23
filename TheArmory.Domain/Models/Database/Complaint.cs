namespace TheArmory.Domain.Models.Database;

/// <summary>
/// Избранное объявление
/// </summary>
public class Complaint : DbEntity
{
    /// <summary>
    /// Описание жалобы
    /// </summary>
    public string Description { get; set; }
    
    // foreign key
    
    /// <summary>
    /// Объявление 
    /// </summary>
    public Guid AdId { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public Guid UserId { get; set; }
    
    // virtual 
    public virtual Ad Ad { get; set; }
    
    public virtual User User { get; set; }
}