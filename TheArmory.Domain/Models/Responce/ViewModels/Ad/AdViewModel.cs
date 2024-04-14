using System;
using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Responce.ViewModels.Media;

namespace TheArmory.Domain.Models.Responce.ViewModels.Ad
{
    public class AdViewModel
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
        /// Дата последнего посещения
        /// </summary>
        [JsonPropertyName("lastVisitDate")]
        public DateTime LastVisitDate { get; set; }
        
        /// <summary>
        /// Ссылка на ютуб видео с обзором
        /// </summary>
        [JsonPropertyName("youTubeLink")]
        public string? YouTubeLink { get; set; }
        
        /// <summary>
        /// Состояние
        /// </summary>
        [JsonPropertyName("condition")]
        public string Condition { get; set; }
        
        /// <summary>
        /// Регион
        /// </summary>
        [JsonPropertyName("region")]
        public string Region { get; set; }
        
        [JsonPropertyName("images")]
        public List<MediaInfoViewModel> Images { get; set; }

        public AdViewModel(){}
        
        public AdViewModel(Database.Ad ad)
        {
            Id = ad.Id;
            Name = ad.Name;
            Price = ad.Price;
            OldPrice = ad.OldPrice;
            Description = ad.Description;
            CreationDateTime = ad.CreationDateTime;
            CountOfViews = ad.CountOfViews;
            CountOfViewsToday = ad.CountOfViewsToday;
            LastVisitDate = ad.LastVisitDate;
            YouTubeLink = ad.YouTubeLink;
            Condition = ad.Condition.Name;
            Region = ad.Region.Name;
            Images = ad.Medias.Select(s => new MediaInfoViewModel(ad, s)).ToList();
        }
    }
}
