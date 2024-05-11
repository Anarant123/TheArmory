using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<ConditionsService>();
builder.Services.AddTransient<RegionsService>();
builder.Services.AddTransient<AdsService>();
builder.Services.AddTransient<ContactsService>();
builder.Services.AddTransient<AdminsService>();
builder.Services.AddTransient<ComplaintsService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.MaxDepth = 2;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "Cookies";
        options.DefaultScheme = "Cookies";
    })
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Domain = builder.Configuration.GetSection("CookieDomain").Value ?? $"localhost";
        options.Cookie.HttpOnly = false;
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.Name = builder.Configuration.GetSection("CookieName").Value ?? $"{new Random().Next()}";
        options.Cookie.SameSite = SameSiteMode.None;
        options.SlidingExpiration = true;
        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        options.Cookie.Path = "/";
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });


builder.Services.TryAddSingleton(s => new BaseUrlOptions
{
    BaseApiUrl =
        builder.Configuration.GetValue("ApiWebAddress",
            "https://192.168.20.89:8443") 
});

builder.Services.AddRazorPages();

builder.Services.AddHttpClient("MyNamedClient", client =>
{
    client.Timeout = TimeSpan.FromHours(12);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/", () => Results.Redirect("/Ads/Index"));

app.Run();