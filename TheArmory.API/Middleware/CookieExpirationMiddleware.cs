namespace TheArmory.Middleware;

public class CookieExpirationMiddleware
{
    private readonly RequestDelegate _next;

    public CookieExpirationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            if (context.User.Identity.IsAuthenticated && context.Request.Cookies.TryGetValue(".AspNetCore.Cookies", out string cookieValue) && !string.IsNullOrEmpty(cookieValue))
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30),
                    Secure = true,
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Path = "/"
                };

                context.Response.Cookies.Append(
                    ".AspNetCore.Cookies",
                    cookieValue,
                    cookieOptions);
            }

            return Task.CompletedTask;
        });

        await _next(context);
    }
}
