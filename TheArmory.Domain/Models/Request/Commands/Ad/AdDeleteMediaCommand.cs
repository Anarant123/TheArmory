using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Request.Commands.Ad;

public class AdDeleteMediaCommand : AdCommand
{
    [JsonPropertyName("mediaId")]
    public Guid MediaId { get; set; }
}