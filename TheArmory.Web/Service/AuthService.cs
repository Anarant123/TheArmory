using System.Net.Http.Headers;
using System.Text.Json;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
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
    
    public async Task<BaseResult<User>> Login(UserLoginCommand command)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl("Auth")}/Login";
            using var content = new StringContent(JsonSerializer.Serialize(command), MediaTypeHeaderValue.Parse("application/json-patch+json"));
            var response = await httpClient.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                return new BaseResult<User>(await response.Content.ReadAsStringAsync());
            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<BaseResult<User>>(responseStream);
            return result ?? new BaseResult<User>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseResult<User>(exception.Message);
        }
    }
    
    public async Task<BaseResult> Registration(UserCreateCommand command)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl("Auth")}/Registration";
            using var content = new StringContent(JsonSerializer.Serialize(command), MediaTypeHeaderValue.Parse("application/json-patch+json"));
            var response = await httpClient.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                return new BaseResult<User>(await response.Content.ReadAsStringAsync());
            var responseStream = await response.Content.ReadAsStreamAsync();
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