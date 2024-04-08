using System.Text.Json.Serialization;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Utils;

namespace TheArmory.Domain.Models.Responce.Result.BaseResult;

public class BaseQueryResult<T> : global::TheArmory.Domain.Models.Responce.Result.BaseResult.BaseResult
{
    /// <summary>
    /// Номер страницы запроса
    /// </summary>
    [JsonPropertyName("pageNumber")]
    public int PageNumber { get; set; }
    
    /// <summary>
    /// Общее количество страниц
    /// </summary>
    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
    
    /// <summary>
    /// Количество элементов на странице
    /// </summary>
    [JsonPropertyName("itemsCount")]
    public int ItemsCount { get; set; }
    
    /// <summary>
    /// Общее количество элементов
    /// </summary>
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    /// <summary>
    /// Возвращаемые объекты
    /// </summary>
    [JsonPropertyName("items")]
    public IEnumerable<T> Items { get; set; } = default!;

    public BaseQueryResult() {}

    public BaseQueryResult(string error)
    {
        Error = error;
    }
    
    public BaseQueryResult(List<T> items)
    {
        Items = items;
        Success = true;
        PageNumber = 1;
        TotalPages = 1;
        TotalCount = items.Count;
        ItemsCount = Items.Count();
    }
    
    /// <summary>
    /// Идет усечение по QueryParams при помощи Take и Skip
    /// </summary>
    /// <param name="items"></param>
    /// <param name="queryParams"></param>
    public BaseQueryResult(List<T> items, BaseQueryItemsParams queryParams)
    {
        Items = items;
        Success = true;
        PageNumber = queryParams.PageNumber;
        TotalPages = queryParams.GetTotalPages(items.Count);
        TotalCount = items.Count;

        Items = items
            .Skip(queryParams.ItemsOnPage * (queryParams.PageNumber - 1))
            .Take(queryParams.ItemsOnPage);
        
        ItemsCount = Items.Count();
    }
    /// <summary>
    /// Нет усечения по количеству
    /// </summary>
    /// <param name="items"></param>
    /// <param name="totalItemsCount"></param>
    /// <param name="queryParams"></param>
    public BaseQueryResult(List<T> items, int totalItemsCount, BaseQueryItemsParams queryParams)
    {
        Items = items;
        Success = true;
        PageNumber = queryParams.PageNumber;
        TotalPages = queryParams.GetTotalPages(totalItemsCount);
        TotalCount = totalItemsCount;

        ItemsCount = Items.Count();
    }
}