using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

public class Media : DbEntity
{
    /// <summary>
    /// Название файла
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    // foreign key
    
    /// <summary>
    /// Id объявления
    /// </summary>
    public Guid AdId { get; set; }
    
    // virtual 
    [JsonIgnore]
    public virtual Ad Ad { get; set; }
}