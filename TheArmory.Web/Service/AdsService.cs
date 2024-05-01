using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Web.Models;
using TheArmory.Web.Utils;

namespace TheArmory.Web.Service;

public class AdsService : BaseService<Ad>
{
    public AdsService(IHttpClientFactory httpClientFactory,
        BaseUrlOptions baseUrlOptions,
        ILogger<BaseService<Ad>> logger) :
        base(httpClientFactory.CreateClient("httpClient"), baseUrlOptions, logger)
    {
    }

    public async Task<BaseQueryResult<TileAdViewModel>> GetMyAds(MyTileAdQueryItemsParams queryItemsParams)
    {
        try
        {
            var uriBuilder = new UriBuilder($"{baseUrlOptions.GetFullApiUrl(RootPointName)}/My");
            var queryParams = queryItemsParams.ToDictionary();
            var queryString = queryParams.ToGetParameters();
            uriBuilder.Query = queryString;

            var response = await httpClient.GetAsync(uriBuilder.Uri);
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

    public async Task<BaseQueryResult<TileAdViewModel>> GetAds(TileAdQueryItemsParams queryItemsParams)
    {
        try
        {
             var uriBuilder = new UriBuilder($"{baseUrlOptions.GetFullApiUrl(RootPointName)}");
            var queryParams = queryItemsParams.ToDictionary();
            var queryString = queryParams.ToGetParameters();
            uriBuilder.Query = queryString;
            var response = await httpClient.GetAsync(uriBuilder.Uri);
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
    
    public async Task<BaseResult<AdViewModel>> Select(AdSelectCommand command)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Select";
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

    public async Task<BaseResult<AdViewModel>> GetSelected()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Selected";
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
    
    public async Task<BaseResult<MyAdViewModel>> GetMyAd(Guid id)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/My/{id}";
            var response = await httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new BaseResult<MyAdViewModel>(await response.Content.ReadAsStringAsync());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<BaseResult<MyAdViewModel>>(responseStream);
            return result ?? new BaseResult<MyAdViewModel>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseResult<MyAdViewModel>(exception.Message);
        }
    }
    
    public async Task<BaseResult<AdPublishInfoViewModel>> GetPublishInformation()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/PublishInformation";
            var response = await httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new BaseResult<AdPublishInfoViewModel>(await response.Content.ReadAsStringAsync());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<BaseResult<AdPublishInfoViewModel>>(responseStream);
            return result ?? new BaseResult<AdPublishInfoViewModel>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseResult<AdPublishInfoViewModel>(exception.Message);
        }
    }
    
    public async Task<BaseResult<AdFilterViewModel>> GetFilterViewModel()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/FilterViewModel";
            var response = await httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new BaseResult<AdFilterViewModel>(await response.Content.ReadAsStringAsync());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<BaseResult<AdFilterViewModel>>(responseStream);
            return result ?? new BaseResult<AdFilterViewModel>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseResult<AdFilterViewModel>(exception.Message);
        }
    }

    public async Task<BaseResult<AdViewModel>> PostAd(AdCreateCommand command)
{
    try
    {
        var url = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}";
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Name), "name");
        formData.Add(new StringContent(command.Price.ToString(CultureInfo.InvariantCulture)), "price");
        formData.Add(new StringContent(command.Description ?? ""), "description");
        formData.Add(new StringContent(command.YouTubeLink ?? ""), "youtubeLink");
        formData.Add(new StringContent(command.ConditionId.ToString()), "conditionId");
        formData.Add(new StringContent(command.Address), "address");
        formData.Add(new StringContent(command.Latitude), "latitude");
        formData.Add(new StringContent(command.Longitude), "longitude");
        formData.Add(new StringContent(command.CategoryId.ToString()), "categoryId");
        if (command.CaliberId.HasValue)
            formData.Add(new StringContent(command.CaliberId.Value.ToString()), "caliberId");
        if (command.WeaponTypeId.HasValue)
            formData.Add(new StringContent(command.WeaponTypeId.Value.ToString()), "weaponTypeId");
        if (command.BarrelPositionId.HasValue)
            formData.Add(new StringContent(command.BarrelPositionId.Value.ToString()), "barrelPositionId");
        if (command.YearOfProduction.HasValue)
            formData.Add(new StringContent(command.YearOfProduction.Value.ToString()), "yearOfProduction");
        foreach (var photo in command.Photos)
        {
            var streamContent = new StreamContent(photo.OpenReadStream());
            formData.Add(streamContent, "photos", photo.FileName);
        }

        var response = await httpClient.PostAsync(url, formData);
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

    
    public async Task<BaseResult> ToFavorites()
    {
        // todo сделать аналогичное возвращение ошибки для остальных сервисов
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/ToFavorite";
            var response = await httpClient.PostAsync(uri, null);
            var responseStream = await response.Content.ReadAsStreamAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errorResult = await JsonSerializer.DeserializeAsync<BaseResult>(responseStream);
                return errorResult ?? new BaseResult(await response.Content.ReadAsStringAsync());
            }

            var result = await JsonSerializer.DeserializeAsync<BaseResult>(responseStream);
            return result ?? new BaseResult(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseResult(exception.Message);
        }
    }
    
    public async Task<BaseResult> Complaint(AdToComplaintCommand command)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Complaint";
            using var content = new StringContent(JsonSerializer.Serialize(command), MediaTypeHeaderValue.Parse("application/json-patch+json"));
            var response = await httpClient.PostAsync(uri, null);
            if (!response.IsSuccessStatusCode)
                return new BaseResult(await response.Content.ReadAsStringAsync());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<BaseResult>(responseStream);
            return result ?? new BaseResult(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseResult(exception.Message);
        }
    }
}