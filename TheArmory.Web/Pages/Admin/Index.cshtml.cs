using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Admin;

public class Index : PageModel
{
    private readonly AdsService _adsService;
    public readonly string BaseUrl;

    [BindProperty] public BaseQueryItemsParams QueryParams { get; set; } = new BaseQueryItemsParams();
    [BindProperty] public BaseQueryResult<TileAdComplaintViewModel> QueryResult { get; set; }

    [BindProperty]
    public List<TileAdComplaintViewModel>? TileAds => QueryResult.Success
        ? QueryResult.Items as List<TileAdComplaintViewModel>
        : new List<TileAdComplaintViewModel>();

    public Index(
        AdsService adsService,
        BaseUrlOptions baseUrlOptions)
    {
        _adsService = adsService;
        BaseUrl = baseUrlOptions.GetFullApiUrl("Files");
    }

    public async Task<ActionResult> OnGetAsync()
    {
        QueryResult = await _adsService.GetComplaintsAds(QueryParams);
        if (!QueryResult.Success)
        {
        }

        return Page();
    }

    public async Task<ActionResult> OnPonsAsync()
    {
        return await OnGetAsync();
    }
}