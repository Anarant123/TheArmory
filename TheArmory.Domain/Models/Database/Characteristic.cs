using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

public class Characteristic : DbEntity
{
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [Column("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [Column("adId")] [JsonIgnore] public Guid AdId { get; set; }

    // virtual

    [JsonIgnore] public virtual Ad Ad { get; set; }
}