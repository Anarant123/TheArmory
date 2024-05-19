using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace TheArmory.Domain.Models.Request.Commands.Ad;

public class AdAddMediaCommand : AdCommand
{
    [JsonIgnore] public IFormFile Photo { get; set; }
}