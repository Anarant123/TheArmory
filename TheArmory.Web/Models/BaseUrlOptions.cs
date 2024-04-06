namespace TheArmory.Web.Models;

public class BaseUrlOptions
{
    /// <summary>
    /// Адрес до API сервера
    /// </summary>
    public string? BaseApiUrl
    {
        private get;
        set;
    }

    public string GetFullApiUrl(string rootPointName)
    {
        if (string.IsNullOrEmpty(BaseApiUrl))
            throw new InvalidOperationException("BaseApiUrl is not set");
        if (string.IsNullOrEmpty(rootPointName))
            throw new InvalidOperationException("rootPointName is not set");
        return $"{BaseApiUrl}/{rootPointName}";
    }
}