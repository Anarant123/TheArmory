namespace TheArmory.Domain.Models.Database;

public class Media : DbEntity
{
    /// <summary>
    /// Название файла
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    // foreign key
    // todo заполнить на основе виртуальных свойств ниже
    
    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid AdId { get; set; }
    
    // virtual 
    
    public virtual Ad Ad { get; set; }
}