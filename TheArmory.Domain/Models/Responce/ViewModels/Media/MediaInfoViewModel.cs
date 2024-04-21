using System;
using System.IO;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Responce.ViewModels.Media;

public class MediaInfoViewModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Локальный путь до файла
    /// </summary>
    [JsonPropertyName("localPath")]
    public string LocalPath { get; set; }
    
    public MediaInfoViewModel() {}

    public MediaInfoViewModel(Database.Ad ad, Database.Media media)
    {
        Id = media.Id;
        LocalPath = Path.Combine(ad.UserId.ToString(), "Ads", ad.Id.ToString(), media.Name);
    }
}