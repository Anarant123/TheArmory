using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ContactCreateCommand
{
    [Required(ErrorMessage = "Поле 'Name' не может быть пустым.")]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Поле 'Description' не может быть пустым.")]
    [JsonPropertyName("description")]
    public string Description { get; set; }
}