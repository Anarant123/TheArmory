using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;
using TheArmory.Repository;
using TheArmory.Utils.Initializer;

var builder = WebApplication.CreateBuilder(args);

// Регистрация контекста базы данных
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Включение поддержки Legacy Timestamp Behavior для Npgsql
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.AddTransient<AdsRepository>();
builder.Services.AddTransient<AuthorizationsRepository>();
builder.Services.AddTransient<UsersRepository>();
builder.Services.AddTransient<PasswordHasher<User>>();


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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "TheArmory Rest API",
            Description = ""
        });
        
        c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TheArmory.Domain.xml"));
        c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TheArmory.APIAPI.xml"));
    }
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheArmory Rest API V1");
    });
}

app.UseRouting();
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

using var scope = app.Services.CreateScope();
await using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
// await context.Database.EnsureCreatedAsync();
await ConditionInitializer.InitializeAsync(context);
await RegionInitializer.InitializeAsync(context);
await RoleInitializer.InitializeAsync(context);
await StatusInitializer.InitializeAsync(context);

app.MapControllers();

app.Run(); 