using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Responce.ViewModels.Region;

public class RegionListViewModel
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    [JsonPropertyName("id")]
    public virtual Guid Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Код региона
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }

    public RegionListViewModel()
    {
    }

    public RegionListViewModel(Database.Region region)
    {
        Id = region.Id;
        Name = region.Name;
        Code = region.Code;
    }
}