﻿using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Responce.ViewModels.Media;
using TheArmory.Domain.Models.Responce.ViewModels.User;

namespace TheArmory.Domain.Models.Responce.ViewModels.Ad;

public class MyAdViewModel
{
    /// <summary>
    /// Id Сущности
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    /// <summary>
    /// Старая цена
    /// </summary>
    [JsonPropertyName("oldPrice")]
    public decimal? OldPrice { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    [JsonPropertyName("creationDateTime")]
    public DateTime CreationDateTime { get; set; }

    /// <summary>
    /// Количество просмотров
    /// </summary>
    [JsonPropertyName("countOfViews")]
    public int CountOfViews { get; set; } = 0;

    /// <summary>
    /// Количество просмотров за сегодня
    /// </summary>
    [JsonPropertyName("countOfViewsToday")]
    public int CountOfViewsToday { get; set; }

    /// <summary>
    /// Ссылка на ютуб видео с обзором
    /// </summary>
    [JsonPropertyName("youTubeLink")]
    public string? YouTubeLink { get; set; }

    /// <summary>
    /// Состояние
    /// </summary>
    [JsonPropertyName("conditionId")]
    public WeaponCondition ConditionId { get; set; }

    /// <summary>
    /// Фотографии
    /// </summary>
    [JsonPropertyName("images")]
    public List<MediaInfoViewModel> Images { get; set; }

    /// <summary>
    /// Геолокация
    /// </summary>
    [JsonPropertyName("location")]
    public Location Location { get; set; }

    /// <summary>
    /// Контакты пользователя
    /// </summary>
    [JsonPropertyName("user")]
    public UserContactsViewModel User { get; set; }

    [JsonPropertyName("categoryId")] public Guid CategoryId { get; set; }

    [JsonPropertyName("caliberId")] public Guid CaliberId { get; set; }

    [JsonPropertyName("weaponTypeId")] public Guid WeaponTypeId { get; set; }

    [JsonPropertyName("barrelPositionId")] public Guid BarrelPositionId { get; set; }

    [JsonPropertyName("yearOfProduction")] public int YearOfProduction { get; set; } = 0;

    [JsonPropertyName("isFavorite")] public bool IsFavorite { get; set; } = false;

    [JsonPropertyName("isComplaint")] public bool IsComplaint { get; set; } = false;

    public MyAdViewModel()
    {
    }

    public MyAdViewModel(Database.Ad ad)
    {
        Id = ad.Id;
        Name = ad.Name;
        Price = ad.Price;
        OldPrice = ad.OldPrice;
        Description = ad.Description;
        CreationDateTime = ad.CreationDateTime;

        CountOfViews = ad.CountOfViews;
        CountOfViewsToday = ad.CountOfViewsToday;

        YouTubeLink = ad.YouTubeLink;
        ConditionId = ad.ConditionId;
        Images = ad.Medias.Select(s => new MediaInfoViewModel(ad, s)).ToList();
        User = new UserContactsViewModel(ad.User);
        Location = ad.Location;
        CategoryId = ad.CategoryId;
    }

    public MyAdViewModel(Database.Ad ad, bool isFavorite, bool isComplaint)
    {
        Id = ad.Id;
        Name = ad.Name;
        Price = ad.Price;
        OldPrice = ad.OldPrice;
        Description = ad.Description;
        CreationDateTime = ad.CreationDateTime;

        CountOfViews = ad.CountOfViews;
        CountOfViewsToday = ad.CountOfViewsToday;

        YouTubeLink = ad.YouTubeLink;
        ConditionId = ad.ConditionId;
        Images = ad.Medias.Select(s => new MediaInfoViewModel(ad, s)).ToList();
        User = new UserContactsViewModel(ad.User);
        Location = ad.Location;
        CategoryId = ad.CategoryId;

        IsFavorite = isFavorite;
        IsComplaint = isComplaint;
    }

    public MyAdViewModel(Database.Ad ad, Characteristic characteristic, bool isFavorite, bool isComplaint)
    {
        Id = ad.Id;
        Name = ad.Name;
        Price = ad.Price;
        OldPrice = ad.OldPrice;
        Description = ad.Description;
        CreationDateTime = ad.CreationDateTime;

        CountOfViews = ad.CountOfViews;
        CountOfViewsToday = ad.CountOfViewsToday;

        YouTubeLink = ad.YouTubeLink;
        ConditionId = ad.ConditionId;
        Images = ad.Medias.Select(s => new MediaInfoViewModel(ad, s)).ToList();
        User = new UserContactsViewModel(ad.User);
        Location = ad.Location;
        CategoryId = ad.CategoryId;

        CaliberId = characteristic.CaliberId;
        WeaponTypeId = characteristic.WeaponTypeId;
        BarrelPositionId = characteristic.BarrelPositionId;
        YearOfProduction = characteristic.YearOfProduction;

        IsFavorite = isFavorite;
        IsComplaint = isComplaint;
    }
}