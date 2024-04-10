using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Responce.ViewModels;

namespace TheArmory.Web.Utils;

public class AuthUtils
{
    /// <summary>
    /// Установка утвердений claims
    /// </summary>
    /// <param name="user"></param>
    /// <param name="isPersistent"></param>
    public static async Task SetLoginClaims(User user, HttpContext httpContext, bool isPersistent = false)
    {
        var claims = new List<Claim>()
        {
            new("Id", user?.Id.ToString() ?? string.Empty),
            new("Phone", user?.PhoneNumber ?? string.Empty),
            new("Email", user?.Email ?? string.Empty),
            new("Role", user?.RoleId.ToString() ?? string.Empty),
            new("Status", user?.StatusId.ToString() ?? string.Empty)
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties()
        {
            IsPersistent = isPersistent
        };

        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }
}