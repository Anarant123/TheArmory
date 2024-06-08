using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Database;

namespace TheArmory.Domain.Models.Responce.ViewModels.Ad;

public class AdPublishInfoViewModel
{
    [JsonPropertyName("conditions")] public List<Database.Condition> Conditions { get; set; }

    [JsonPropertyName("categories")] public List<Category> Categories { get; set; }
}