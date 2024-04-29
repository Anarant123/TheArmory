using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Request.Queries;

public class MyTileAdQueryItemsParams : BaseQueryItemsParams
{
    [JsonPropertyName("statusId")]
    public StateStatus? StatusId { get; set; } = StateStatus.Actively;
}