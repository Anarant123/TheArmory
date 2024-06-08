using System.Text.Json;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Region;
using TheArmory.Web.Models;

namespace TheArmory.Web.Service;

public class RegionsService : BaseService<Region>
{
    public RegionsService(IHttpClientFactory httpClientFactory,
        BaseUrlOptions baseUrlOptions,
        ILogger<BaseService<Region>> logger) :
        base(httpClientFactory.CreateClient("httpClient"), baseUrlOptions, logger)
    {
    }

    public async Task<BaseQueryResult<RegionListViewModel>> GetSelectList()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/SelectList";
            var response = await httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new BaseQueryResult<RegionListViewModel>(await response.Content.ReadAsStringAsync());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<BaseQueryResult<RegionListViewModel>>(responseStream);
            return result ?? new BaseQueryResult<RegionListViewModel>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseQueryResult<RegionListViewModel>(exception.Message);
        }
    }
}