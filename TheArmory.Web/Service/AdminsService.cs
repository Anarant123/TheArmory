using System.Globalization;
using System.Text.Json;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.User;
using TheArmory.Web.Models;
using TheArmory.Web.Utils;

namespace TheArmory.Web.Service;

public class AdminsService : BaseService<User>
{
    public AdminsService(IHttpClientFactory httpClientFactory,
        BaseUrlOptions baseUrlOptions,
        ILogger<BaseService<User>> logger) :
        base(httpClientFactory.CreateClient("httpClient"), baseUrlOptions, logger)
    {
    }

    public async Task<BaseQueryResult<UserViewModel>> GetAdmins(BaseQueryItemsParams queryItemsParams)
    {
        try
        {
            var uriBuilder = new UriBuilder($"{baseUrlOptions.GetFullApiUrl("Admins")}/Admins");
            var queryParams = queryItemsParams.ToDictionary();
            var queryString = queryParams.ToGetParameters();
            uriBuilder.Query = queryString;
            var response = await httpClient.GetAsync(uriBuilder.Uri);
            if (!response.IsSuccessStatusCode)
                return new BaseQueryResult<UserViewModel>(await response.Content.ReadAsStringAsync());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<BaseQueryResult<UserViewModel>>(responseStream);
            return result ?? new BaseQueryResult<UserViewModel>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseQueryResult<UserViewModel>(exception.Message);
        }
    }
    public async Task<BaseResult> Registration(UserAdminCreateCommand command)
    {
        try
        {
            var url = $"{baseUrlOptions.GetFullApiUrl("Admins")}/Registration";
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(command.Login ?? ""), "login");
            formData.Add(new StringContent(command.Name ?? ""), "name");
            formData.Add(new StringContent(command.Password ?? ""), "password");
            formData.Add(new StringContent(command.PasswordConfirm ?? ""), "passwordConfirm");
            var streamContent = new StreamContent(command.Photo.OpenReadStream());
            formData.Add(streamContent, "photo", (command.Photo.FileName));

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