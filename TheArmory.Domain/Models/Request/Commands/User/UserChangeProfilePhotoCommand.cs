using Microsoft.AspNetCore.Http;

namespace TheArmory.Domain.Models.Request.Commands.User;

public class UserChangeProfilePhotoCommand
{
    public IFormFile Photo { get; set; }
}