using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Responce.Result.BaseResult;

public abstract class BaseResult
{
    /// <summary>
    /// Успешное выполнение запроса
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    
    /// <summary>
    /// Строка с подробностями об ошибке
    /// </summary>
    [JsonPropertyName("error")]
    public string Error { get; set; }
}

public class BaseResult<T> : BaseResult
{
    /// <summary>
    /// Результат запроса
    /// </summary>
    [JsonPropertyName("item")]
    public T Item { get; set; }
}