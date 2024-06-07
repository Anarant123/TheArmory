using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Request.Commands.Characteristic;

namespace TheArmory.Domain.Models.Request.Commands.Ad
{
    public class AdCreateCommand
    {
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
        /// Описание
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Ссылка на YouTube видео с обзором
        /// </summary>
        [JsonPropertyName("youtubeLink")]
        public string? YouTubeLink { get; set; } = string.Empty;

        /// <summary>
        /// Id Состояния
        /// </summary>
        [JsonPropertyName("conditionId")]
        public WeaponCondition ConditionId { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; } = string.Empty;

        /// <summary>
        /// Широта
        /// </summary>
        [JsonPropertyName("latitude")]
        public string? Latitude { get; set; } = string.Empty;

        /// <summary>
        /// Долгота
        /// </summary>
        [JsonPropertyName("longitude")]
        public string? Longitude { get; set; } = string.Empty;

        /// <summary>
        /// Id Категории
        /// </summary>
        [JsonPropertyName("CategoryId")]
        public Guid CategoryId { get; set; } = Guid.Empty;

        /// <summary>
        /// Набор характеристик
        /// </summary>
        [JsonPropertyName("сharacteristics")] 
        public List<CharacteristicCreateCommand> Characteristics { get; set; } = new List<CharacteristicCreateCommand>();

        /// <summary>
        /// Фотографии
        /// </summary>
        [JsonPropertyName("photos")]
        public List<IFormFile> Photos { get; set; } = new List<IFormFile>();
    }
}
