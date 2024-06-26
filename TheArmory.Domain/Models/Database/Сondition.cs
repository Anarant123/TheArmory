﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Domain.Models.Database;

/// <summary>
/// Состояние оружия
/// </summary>
public class Condition : DbEntity
{
    [Key]
    [Column("id")]
    [JsonPropertyName("id")]
    public WeaponCondition Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    [Column("name")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    // virtual
    [JsonIgnore] public virtual List<Ad> Ads { get; set; }

    public Condition(WeaponCondition id, string name)
    {
        Id = id;
        Name = name;
    }
}