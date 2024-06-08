using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

public class Location : DbEntity
{
    /// <summary>
    /// Адресс
    /// </summary>
    [Column("address")]
    [JsonPropertyName("address")]
    public string Address { get; set; }

    /// <summary>
    /// Широта
    /// </summary>
    [Column("latitude")]
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    /// <summary>
    /// Долгота
    /// </summary>
    [Column("longitude")]
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    // foreign key

    /// <summary>
    /// Id объявления
    /// </summary>
    [Column("adId")]
    [JsonPropertyName("adId")]
    public Guid AdId { get; set; }

    // virtual

    [JsonIgnore] public virtual Ad Ad { get; set; }
}