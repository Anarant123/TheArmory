using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Responce.ViewModels;
using TheArmory.Domain.Models.Responce.ViewModels.User;

namespace TheArmory.Utils;

public class AuthUtils
{
    /// <summary>
    /// Установка утвердений claims
    /// </summary>
    /// <param name="user"></param>
    /// <param name="isPersistent"></param>
    public static async Task SetLoginClaims(UserViewModel user, HttpContext httpContext, bool isPersistent = false)
    {
        var claims = new List<Claim>()
        {
            new("Id", user?.Id.ToString() ?? string.Empty),
            new("Login", user?.Login ?? string.Empty),
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