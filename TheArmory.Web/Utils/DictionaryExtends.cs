using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace TheArmory.Web.Utils;

public static class DictionaryExtends
{
    public static Dictionary<string, object> ToDictionary(this object queryParameters)
    {
        var result = new Dictionary<string, object>();
        foreach (var propertyInfo in queryParameters.GetType().GetProperties())
        {
            var propertyValue = propertyInfo.GetValue(queryParameters);
            if (propertyValue is null)
                continue;

            if (propertyValue.GetType().IsArray)
            {
                var resultItem = (Name: propertyInfo.Name, Value: string.Empty);
                foreach (var value in (IEnumerable)propertyValue)
                    if (string.IsNullOrEmpty(resultItem.Value))
                        resultItem.Value += $"{value}";
                    else
                        resultItem.Value += $";;{value}";
                result.Add(resultItem.Name, resultItem.Value);
            }
            else
            {
                if (!string.IsNullOrEmpty(propertyValue.ToString()))
                    result.Add(propertyInfo.Name, propertyInfo.GetValue(queryParameters)?.ToString() ?? string.Empty);
            }
        }

        return result;
    }

    public static Dictionary<string, object>? ToMixedDictionary(this object queryParameters)
    {
        var result = new Dictionary<string, object>();

        foreach (var propertyInfo in queryParameters.GetType().GetProperties())
        {
            var propertyValue = propertyInfo.GetValue(queryParameters);

            if (propertyValue is null)
                continue;

            if (propertyValue.GetType().IsArray)
            {
                var resultItem = (Name: propertyInfo.Name, Value: string.Empty);

                foreach (var value in (IEnumerable)propertyValue)
                {
                    if (string.IsNullOrEmpty(resultItem.Value))
                        resultItem.Value += $"{value}";
                    else
                        resultItem.Value += $";;{value}";
                }

                result.Add(resultItem.Name, resultItem.Value);
            }
            else
            {
                if (propertyValue.GetType() == typeof(int))
                {
                    result.Add(propertyInfo.Name, (int)propertyInfo.GetValue(queryParameters));
                }
                else if (propertyValue.GetType() == typeof(DateTime))
                {
                    result.Add(propertyInfo.Name, ((DateTime)propertyInfo.GetValue(queryParameters)).ToString("yyyy-MM-ddTHH:mm:ss"));
                }
                else
                {
                    result.Add(propertyInfo.Name, propertyInfo.GetValue(queryParameters)?.ToString() ?? string.Empty);
                }
            }
        }

        return result;
    }

    public static string ToGetParameters<T, TS>(this Dictionary<T, TS> parameters) where T : notnull
    {
        if (parameters is null or { Count: 0 }) return string.Empty;

        var s = new StringBuilder();
        foreach (var parameter in parameters)
            if (parameter.Value?.ToString()?.Contains(";;") == true)
                foreach (var parameterValue in parameter.Value?.ToString()?.Split(";;")!)
                    s.Append($"&{parameter.Key}={parameterValue}");
            else
                s.Append($"&{parameter.Key}={parameter.Value}");

        return s.ToString();
    }

    public static string GetDisplayNameAttribute(this Enum enumValue)
    {
        return enumValue
            .GetType()
            .GetMember(enumValue.ToString())
            .First()?
            .GetCustomAttribute<DisplayAttribute>()?
            .Name ?? enumValue.ToString();
    }
}