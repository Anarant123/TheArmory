using System.Text.Json.Serialization;

namespace TheArmory.Domain.Models.Responce.Result.BaseResult;

public class BaseResult
{
    public BaseResult(){}
    public BaseResult(string error)
    {
        Error = error;
    }

    /// <summary>
    /// Успешное выполнение запроса
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; } = true;
    
    /// <summary>
    /// Строка с подробностями об ошибке
    /// </summary>
    [JsonPropertyName("error")]
    public string Error { get; set; }
}

public class BaseResult<T> : BaseResult
{
    public BaseResult() { }
    
    public BaseResult(T item) 
    {
        Item = item;
    }
    
    public BaseResult(string error)
    {
        Error = error;
        Success = false;
    }
    
    /// <summary>
    /// Результат запроса
    /// </summary>
    [JsonPropertyName("item")]
    public T Item { get; set; }
}

