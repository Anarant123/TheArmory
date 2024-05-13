using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Request.Commands.Ad;

public class AdChangeYouTubeLinkCommand : AdCommand
{
    /// <summary>
    /// Ссылка на ютуб видео с обзором
    /// </summary>
    [JsonPropertyName("youTubeLink")]
    public string? YouTubeLink { get; set; }
}