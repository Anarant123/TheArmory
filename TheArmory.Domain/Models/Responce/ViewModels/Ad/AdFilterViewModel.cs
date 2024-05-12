using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Database;

namespace TheArmory.Domain.Models.Responce.ViewModels.Ad;

public class AdFilterViewModel
{
    [JsonPropertyName("regions")]
    public List<Database.Region> Regions { get; set; }
    
    [JsonPropertyName("conditions")]
    public List<Database.Condition> Conditions { get; set; }

    [JsonPropertyName("categories")]
    public List<Category> Categories { get; set; }
}