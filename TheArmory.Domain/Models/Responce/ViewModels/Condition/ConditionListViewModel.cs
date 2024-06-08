using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Responce.ViewModels.Condition;

public class ConditionListViewModel
{
    [JsonPropertyName("id")] public WeaponCondition Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;


    public ConditionListViewModel()
    {
    }

    public ConditionListViewModel(Database.Condition condition)
    {
        Id = condition.Id;
        Name = condition.Name;
    }
}