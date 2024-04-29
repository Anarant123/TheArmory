using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Request.Queries;

public class TileAdQueryItemsParams : BaseQueryItemsParams
{
    [JsonPropertyName("statusId")]
    public StateStatus StatusId { get; set; } = StateStatus.Actively;
    
    [JsonPropertyName("regionId")]
    public Guid? RegionId { get; set; } = Guid.Empty;
    
    [JsonPropertyName("categoryId")]
    public Guid CategoryId { get; set; } = Guid.Empty;

    [JsonPropertyName("caliberId")]
    public Guid CaliberId { get; set; } = Guid.Empty;

    [JsonPropertyName("weaponTypeId")]
    public Guid WeaponTypeId { get; set; } = Guid.Empty;

    [JsonPropertyName("barrelPositionId")]
    public Guid BarrelPositionId { get; set; } = Guid.Empty;

    [JsonPropertyName("priceFrom")] 
    public decimal PriceFrom { get; set; } = 0;
    
    [JsonPropertyName("priceFrom")] 
    public decimal PriceTo { get; set; }  = 10000000;
}