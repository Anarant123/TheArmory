using System.Net.Http.Headers;
using System.Text.Json;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.Region;
using TheArmory.Web.Models;

namespace TheArmory.Web.Service;

public class AdsService : BaseService<Ad>
{
    public AdsService(IHttpClientFactory httpClientFactory,
        BaseUrlOptions baseUrlOptions,
        ILogger<BaseService<Ad>> logger) :
        base(httpClientFactory.CreateClient("httpClient"), baseUrlOptions, logger)
    {
    }

    public async Task<BaseQueryResult<TileAdViewModel>> GetAds()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}";
            var response = await httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new BaseQueryResult<TileAdViewModel>(await response.Content.ReadAsStringAsync());
            
            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<BaseQueryResult<TileAdViewModel>>(responseStream);
            return result ?? new BaseQueryResult<TileAdViewModel>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseQueryResult<TileAdViewModel>(exception.Message);
        }
    }
    
    public async Task<BaseResult<AdViewModel>> GetAd(Guid id)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/{id}";
            var response = await httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new BaseResult<AdViewModel>(await response.Content.ReadAsStringAsync());
            
            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<BaseResult<AdViewModel>>(responseStream);
            return result ?? new BaseResult<AdViewModel>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseResult<AdViewModel>(exception.Message);
        }
    }
    
    public async Task<BaseResult<AdViewModel>> PostAd(AdCreateCommand command)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}";
            // var formContent = new MultipartFormDataContent();
            //
            // var stream = File.OpenRead(UserImagePath);
            // formContent.Add(new StringContent(user.Id.ToString()), "userId");
            //
            // var imageContent = new StreamContent(stream);
            // imageContent.Headers.ContentType =
            //     MediaTypeHeaderValue.Parse($"image/{UserImagePath.Split('/').Last().Split('.').Last()}");
            // formContent.Add(imageContent, "image", $"{UserImagePath.Split('/').Last()}");
            
            using var content = new StringContent(JsonSerializer.Serialize(command), MediaTypeHeaderValue.Parse("application/json-patch+json"));
            var response = await httpClient.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                return new BaseResult<AdViewModel>(await response.Content.ReadAsStringAsync());
            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<BaseResult<AdViewModel>>(responseStream);
            return result ?? new BaseResult<AdViewModel>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseResult<AdViewModel>(exception.Message);
        }
    }
}