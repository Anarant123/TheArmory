using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Request.Queries;

public class TileAdQueryItemsParams : BaseQueryItemsParams
{
    [JsonPropertyName("regionId")] 
    public Guid? RegionId { get; set; } = null;
    
    [JsonPropertyName("categoryId")]
    public Guid? CategoryId { get; set; } = null;

    [JsonPropertyName("caliberId")]
    public Guid? CaliberId { get; set; } = null;

    [JsonPropertyName("weaponTypeId")]
    public Guid? WeaponTypeId { get; set; } = null;

    [JsonPropertyName("barrelPositionId")]
    public Guid? BarrelPositionId { get; set; } = null;

    [JsonPropertyName("priceFrom")] 
    public decimal PriceFrom { get; set; } = 0;
    
    [JsonPropertyName("priceFrom")] 
    public decimal PriceTo { get; set; }  = decimal.MaxValue;
}