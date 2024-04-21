using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Request.Commands.User;

public class UserChangeNameCommand
{
    [JsonPropertyName("newName")]
    public string NewName { get; set; }
}