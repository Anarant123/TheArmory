using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Request.Commands;

public class EntityCommand
{
    [JsonPropertyName("id")] public Guid Id { get; set; } = Guid.Empty;
}