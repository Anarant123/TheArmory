using TheArmory.Domain.Models.Request.Queries;

namespace TheArmory.Domain.Utils;

public static class QueryParametersExtensions
{
    public static bool HasPrevious(this BaseQueryItemsParams queryParameters)
    {
        return (queryParameters.PageNumber > 1);
    }

    public static bool HasNext(this BaseQueryItemsParams queryParameters, int totalCount)
    {
        if (queryParameters == null) throw new ArgumentNullException(nameof(queryParameters));
        return (queryParameters.PageNumber < GetTotalPages(queryParameters, totalCount));
    }

    public static int GetTotalPages(this BaseQueryItemsParams queryParameters, int totalCount)
    {
        return (int)Math.Ceiling(totalCount / (double)queryParameters.ItemsOnPage);
    }

    public static bool HasQuery(this BaseQueryItemsParams queryParameters)
    {
        return !string.IsNullOrEmpty(queryParameters.FilterText);
    }

    public static bool IsDescending(this BaseQueryItemsParams queryParameters)
    {
        if (!string.IsNullOrEmpty(queryParameters.OrderBy))
        {
            return queryParameters.OrderBy.Split(' ').Last().ToLowerInvariant().StartsWith("desc");
        }
        return false;
    }
}