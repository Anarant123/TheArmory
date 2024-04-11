﻿using TheArmory.Domain.Models.Enums;
using System.Text.Json.Serialization; 

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
        /// Id Региона
        /// </summary>
        [JsonPropertyName("regionId")]
        public Guid RegionId { get; set; }
    }
}
