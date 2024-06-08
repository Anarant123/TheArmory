using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Repository;
using TheArmory.Utils.Initializer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddTransient<MediasRepository>();
builder.Services.AddTransient<AdsRepository>();
builder.Services.AddTransient<AdminsRepository>();
builder.Services.AddTransient<UsersRepository>();
builder.Services.AddTransient<ConditionsRepository>();
builder.Services.AddTransient<RegionsRepository>();
builder.Services.AddTransient<ContactsRepository>();
builder.Services.AddTransient<ComplaintsRepository>();
builder.Services.AddTransient<CharacteristicsRepository>();

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
        options.SlidingExpiration = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.Name = builder.Configuration.GetSection("CookieName").Value ?? $"{new Random().Next()}";
        options.Cookie.SameSite = SameSiteMode.None;
        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        options.Cookie.Path = "/";
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });

builder.Services.AddControllers();
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
        c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TheArmory.API.xml"));
    }
);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // сделайте куки-сессии обязательными
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheArmory Rest API V1");
    });
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});

app.UseSession();
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
await CategoryInitializer.InitializeAsync(context);

app.MapControllers();

app.Run(); 