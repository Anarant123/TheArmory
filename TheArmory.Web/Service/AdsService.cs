using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
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

    public async Task<BaseQueryResult<TileAdViewModel>> GetFavoritesAds(BaseQueryItemsParams queryItemsParams)
    {
        try
        {
            var uriBuilder = new UriBuilder($"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Favorites");
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

    public async Task<BaseQueryResult<TileAdComplaintViewModel>> GetComplaintsAds(BaseQueryItemsParams queryItemsParams)
    {
        try
        {
            var uriBuilder = new UriBuilder($"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Complaints");
            var queryParams = queryItemsParams.ToDictionary();
            var queryString = queryParams.ToGetParameters();
            uriBuilder.Query = queryString;
            var response = await httpClient.GetAsync(uriBuilder.Uri);
            if (!response.IsSuccessStatusCode)
                return new BaseQueryResult<TileAdComplaintViewModel>(await response.Content.ReadAsStringAsync());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var result =
                await JsonSerializer.DeserializeAsync<BaseQueryResult<TileAdComplaintViewModel>>(responseStream);
            return result ?? new BaseQueryResult<TileAdComplaintViewModel>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseQueryResult<TileAdComplaintViewModel>(exception.Message);
        }
    }

    public async Task<BaseResult<AdViewModel>> Select(AdSelectCommand command)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Select";
            using var content = new StringContent(JsonSerializer.Serialize(command),
                MediaTypeHeaderValue.Parse("application/json-patch+json"));
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

    public async Task<BaseResult<MyAdViewModel>> SelectMy(AdSelectCommand command)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/SelectMy";
            using var content = new StringContent(JsonSerializer.Serialize(command),
                MediaTypeHeaderValue.Parse("application/json-patch+json"));
            var response = await httpClient.PostAsync(uri, content);
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

    public async Task<BaseResult<MyAdViewModel>> GetSelectedMy()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/SelectedMy";
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
            if (command.Address != null) formData.Add(new StringContent(command.Address), "address");
            if (command.Latitude != null) formData.Add(new StringContent(command.Latitude), "latitude");
            if (command.Longitude != null) formData.Add(new StringContent(command.Longitude), "longitude");
            formData.Add(new StringContent(command.CategoryId.ToString()), "categoryId");
            var characteristicIndex = 0;
            foreach (var characteristicJson in command.Characteristics.Select(characteristic =>
                         JsonSerializer.Serialize(characteristic)))
            {
                formData.Add(new StringContent(characteristicJson, Encoding.UTF8, "application/json"),
                    $"characteristics[{characteristicIndex}]");
                characteristicIndex++;
            }

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

    public async Task<BaseResult> DeleteFavorite()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Favorite";
            var response = await httpClient.DeleteAsync(uri);
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

    public async Task<BaseResult> DeactivateAd()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Deactivate";
            var response = await httpClient.PutAsync(uri, null);
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

    public async Task<BaseResult> ActivateAd()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Activate";
            var response = await httpClient.PutAsync(uri, null);
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

    public async Task<BaseResult> DeleteAd()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}";
            var response = await httpClient.DeleteAsync(uri);
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
            using var content = new StringContent(JsonSerializer.Serialize(command),
                MediaTypeHeaderValue.Parse("application/json-patch+json"));
            var response = await httpClient.PostAsync(uri, content);
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

    public async Task<BaseResult> Ban()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Ban";
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

    public async Task<BaseResult> Justify()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Justify";
            var response = await httpClient.PutAsync(uri, null);
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

    public async Task<BaseResult<MyAdViewModel>> Update(AdUpdateCommand command)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}";
            using var content = new StringContent(JsonSerializer.Serialize(command),
                MediaTypeHeaderValue.Parse("application/json"));
            var response = await httpClient.PutAsync(uri, content);
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

    public async Task<BaseResult<MyAdViewModel>> ChangeYoutubeLink(AdChangeYouTubeLinkCommand command)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/YouTubeLink";
            using var content = new StringContent(JsonSerializer.Serialize(command),
                MediaTypeHeaderValue.Parse("application/json"));
            var response = await httpClient.PutAsync(uri, content);
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

    public async Task<BaseResult<MyAdViewModel>> ChangeLocation(AdLocationCommand command)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Location";
            using var content = new StringContent(JsonSerializer.Serialize(command),
                MediaTypeHeaderValue.Parse("application/json"));
            var response = await httpClient.PutAsync(uri, content);
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

    public async Task<BaseResult<MyAdViewModel>> DeleteMedia(AdDeleteMediaCommand command)
    {
        try
        {
            var uriBuilder = new UriBuilder($"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Media");
            uriBuilder.Query = $"id={command.Id}&mediaId={command.MediaId}";
            ;
            var response = await httpClient.DeleteAsync(uriBuilder.Uri);
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

    public async Task<BaseResult> AddMedia(AdAddMediaCommand command)
    {
        try
        {
            var url = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Media";
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(command.Id.ToString()), "id");
            var streamContent = new StreamContent(command.Photo.OpenReadStream());
            formData.Add(streamContent, "photo", command.Photo.FileName);

            var response = await httpClient.PostAsync(url, formData);
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