using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Request.Queries;

public class BaseQueryItemsParams : BaseQuery
{
    [JsonIgnore] public static int DefaultItemsCount => 40;
    [JsonIgnore] public static int DefaultPageNumber => 1;
    [JsonIgnore] public static int DefaultOrderByColumn => -1;

    /// <summary>
    /// Номер страницы запроса
    /// </summary>
    [Range(0, int.MaxValue)]
    [JsonPropertyName("pageNumber")]
    public int PageNumber { get; set; } = DefaultPageNumber;

    /// <summary>
    /// Количество элементов на странице
    /// </summary>
    [Range(0, int.MaxValue)]
    [JsonPropertyName("itemsOnPage")]
    public int ItemsOnPage { get; set; } = DefaultItemsCount;

    /// <summary>
    /// Текстовый фильтр для поиска
    /// </summary>
    [JsonPropertyName("filterText")]
    public string? FilterText { get; set; }

    /// <summary>
    /// Сортировка по номеру колонки(свойства)
    /// </summary>
    [JsonPropertyName("orderBy")]
    public string? OrderBy { get; set; }
}