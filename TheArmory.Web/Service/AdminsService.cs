using System.Globalization;
using System.Text.Json;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Web.Models;

namespace TheArmory.Web.Service;

public class AdminsService : BaseService<User>
{
    public AdminsService(IHttpClientFactory httpClientFactory,
        BaseUrlOptions baseUrlOptions,
        ILogger<BaseService<User>> logger) :
        base(httpClientFactory.CreateClient("httpClient"), baseUrlOptions, logger)
    {
    }

    public async Task<BaseResult> Registration(UserAdminCreateCommand command)
    {
        try
        {
            var url = $"{baseUrlOptions.GetFullApiUrl("Auth")}/Registration";
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(command.Login ?? ""), "login");
            formData.Add(new StringContent(command.Password ?? ""), "password");
            var streamContent = new StreamContent(command.Photo.OpenReadStream());
            formData.Add(streamContent, "photos", (command.Photo.FileName));

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