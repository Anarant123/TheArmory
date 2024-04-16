using System.Text.Json;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.User;
using TheArmory.Web.Models;

namespace TheArmory.Web.Service;

public class UserService : BaseService<User>
{
    public UserService(IHttpClientFactory httpClientFactory,
        BaseUrlOptions baseUrlOptions,
        ILogger<BaseService<User>> logger) :
        base(httpClientFactory.CreateClient("httpClient"), baseUrlOptions, logger)
    {
    }
    
    public async Task<BaseResult<UserPersonalInfoViewModel>> GetMe()
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/Me";
            var response = await httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new BaseResult<UserPersonalInfoViewModel>(await response.Content.ReadAsStringAsync());
            
            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<BaseResult<UserPersonalInfoViewModel>>(responseStream);
            return result ?? new BaseResult<UserPersonalInfoViewModel>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseResult<UserPersonalInfoViewModel>(exception.Message);
        }
    }
}