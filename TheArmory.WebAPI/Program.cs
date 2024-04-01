using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TheArmory.Domain.Context;
using TheArmory.Repository;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString), ServiceLifetime.Transient);
builder.Services.AddControllersWithViews(opt => { opt.Filters.Add(new IgnoreAntiforgeryTokenAttribute()); });
builder.Services.AddSession();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<UsersRepository>();
builder.Services.AddTransient<AdsRepository>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.MaxDepth = 2;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
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
            Title = "NewsAggregator Rest API",
            Description = ""
        });
        
        c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NewsAggregator.Domain.xml"));
        c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NewsAggregator.WebAPI.xml"));
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
using var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
await context.Database.EnsureCreatedAsync();

app.Run();