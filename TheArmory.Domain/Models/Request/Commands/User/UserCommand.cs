using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Request.Commands.User;

public class UserCommand
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}