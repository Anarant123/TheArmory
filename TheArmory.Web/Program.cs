using Microsoft.Extensions.DependencyInjection.Extensions;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<AuthService>();


builder.Services.TryAddSingleton(s => new BaseUrlOptions
{
    BaseApiUrl =
        builder.Configuration.GetValue("ApiWebAddress",
            "https://192.168.20.89:8443") 
});

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();