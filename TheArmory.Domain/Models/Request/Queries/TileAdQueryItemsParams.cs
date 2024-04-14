using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Request.Queries;

public class TileAdQueryItemsParams : BaseQueryItemsParams
{
    public Guid? RegionId { get; set; }
    
    public WeaponCondition? ConditionId { get; set; }
    
    public Guid? CategoryId { get; set; }
    
    public decimal? PriceFrom { get; set; } 
    
    public decimal? PriceTo { get; set; } 
}