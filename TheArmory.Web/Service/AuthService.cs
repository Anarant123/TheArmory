using System.Net.Http.Headers;
using System.Text.Json;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.User;
using TheArmory.Web.Models;

namespace TheArmory.Web.Service;

public class AuthService : BaseService<User>
{
    public AuthService(IHttpClientFactory httpClientFactory,
        BaseUrlOptions baseUrlOptions,
        ILogger<BaseService<User>> logger) :
        base(httpClientFactory.CreateClient("httpClient"), baseUrlOptions, logger)
    {
    }
    
    public async Task<BaseResult<UserViewModel>> Login(UserLoginCommand command)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl("Auth")}/Login";
            using var content = new StringContent(JsonSerializer.Serialize(command), MediaTypeHeaderValue.Parse("application/json-patch+json"));
            var response = await httpClient.PostAsync(uri, content);
            var responseStream = await response.Content.ReadAsStreamAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errorResult = await JsonSerializer.DeserializeAsync<BaseResult<UserViewModel>>(responseStream);
                return errorResult ?? new BaseResult<UserViewModel>(await response.Content.ReadAsStringAsync());
            }
            var result = await JsonSerializer.DeserializeAsync<BaseResult<UserViewModel>>(responseStream);
            return result ?? new BaseResult<UserViewModel>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseResult<UserViewModel>(exception.Message);
        }
    }
    
    public async Task<BaseResult> Registration(UserCreateCommand command)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl("Auth")}/Registration";
            using var content = new StringContent(JsonSerializer.Serialize(command), MediaTypeHeaderValue.Parse("application/json-patch+json"));
            var response = await httpClient.PostAsync(uri, content);
            var responseStream = await response.Content.ReadAsStreamAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errorResult = await JsonSerializer.DeserializeAsync<BaseResult<UserViewModel>>(responseStream);
                return errorResult ?? new BaseResult<UserViewModel>(await response.Content.ReadAsStringAsync());
            }
            var result = await JsonSerializer.DeserializeAsync<BaseResult>(responseStream);
            return result ?? new BaseResult(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseResult(exception.Message);
        }
    }
    
    public async Task<BaseResult> Logout()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl("Auth")}/Logout";
            using var content = new StringContent(string.Empty, MediaTypeHeaderValue.Parse("application/json-patch+json"));
            var response = await httpClient.PostAsync(uri, content);
            var responseStream = await response.Content.ReadAsStreamAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errorResult = await JsonSerializer.DeserializeAsync<BaseResult<UserViewModel>>(responseStream);
                return errorResult ?? new BaseResult<UserViewModel>(await response.Content.ReadAsStringAsync());
            }
            var result = await JsonSerializer.DeserializeAsync<BaseResult>(responseStream);
            return result ?? new BaseResult(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseResult(exception.Message);
        }
    }
}