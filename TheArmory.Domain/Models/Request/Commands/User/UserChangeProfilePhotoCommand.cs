using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace TheArmory.Domain.Models.Request.Commands.User;

public class UserChangeProfilePhotoCommand
{
    [JsonIgnore] public IFormFile Photo { get; set; } 
}