using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Database;

/// <summary>
/// Родительская модель любой сущности базы данных
/// </summary>
public abstract class DbEntity
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    [Key]
    [Column("id")]
    [JsonPropertyName("id")]
    public virtual Guid Id { get; set; } = Guid.NewGuid();
}