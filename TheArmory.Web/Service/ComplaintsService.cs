using System.Text.Json;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.Complaint;
using TheArmory.Web.Models;
using TheArmory.Web.Utils;

namespace TheArmory.Web.Service;

public class ComplaintsService : BaseService<Complaint>
{
    public ComplaintsService(IHttpClientFactory httpClientFactory,
        BaseUrlOptions baseUrlOptions,
        ILogger<BaseService<Complaint>> logger) :
        base(httpClientFactory.CreateClient("httpClient"), baseUrlOptions, logger)
    {
    }
    
    public async Task<BaseQueryResult<ComplaintViewModel>> GetComplaints()
    {
        try
        {
            var uriBuilder = new UriBuilder($"{baseUrlOptions.GetFullApiUrl(RootPointName)}");
            var response = await httpClient.GetAsync(uriBuilder.Uri);
            if (!response.IsSuccessStatusCode)
                return new BaseQueryResult<ComplaintViewModel>(await response.Content.ReadAsStringAsync());

            var responseStream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<BaseQueryResult<ComplaintViewModel>>(responseStream);
            return result ?? new BaseQueryResult<ComplaintViewModel>(ErrorsMessage.SomethingWentWrong);
        }
        catch (Exception exception)
        {
            return new BaseQueryResult<ComplaintViewModel>(exception.Message);
        }
    }
}