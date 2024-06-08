using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Request.Commands.Characteristic;

public class CharacteristicCreateCommand
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; }
}