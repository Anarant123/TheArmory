using Microsoft.AspNetCore.Http;

namespace TheArmory.Domain.Models.Request.Commands.Ad;

public class AdAddMediaCommand : AdCommand
{
    public IFormFile Photo { get; set; }
}