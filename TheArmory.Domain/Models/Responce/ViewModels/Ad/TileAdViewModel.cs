using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Responce.ViewModels.Media;

namespace TheArmory.Domain.Models.Responce.ViewModels.Ad;

public class TileAdViewModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("images")]
    public List<MediaInfoViewModel> Images { get; set; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    
    [JsonPropertyName("creationDateTime")]
    public DateTime CreationDateTime { get; set; }
    
    [JsonPropertyName("countOfViews")]
    public int CountOfViews { get; set; } = 0;

    [JsonPropertyName("countOfViewsToday")]
    public int CountOfViewsToday { get; set; } = 0;
    
    [JsonIgnore]
    public string BaseUrl { get; set; } = "";

    public TileAdViewModel(){}
    
    public TileAdViewModel(Database.Ad ad)
    {
        Id = ad.Id;
        Name = ad.Name;
        Images = ad.Medias.Select(s => new MediaInfoViewModel(ad, s)).ToList();
        Price = ad.Price;
        CreationDateTime = ad.CreationDateTime;
        CountOfViews = ad.CountOfViews;
        CountOfViewsToday = ad.CountOfViewsToday;
    }
}