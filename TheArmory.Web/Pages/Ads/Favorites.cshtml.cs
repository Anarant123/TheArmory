using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Ads;

public class Favorites : PageModel
{
    private readonly AdsService _adsService;
    public readonly string BaseUrl;

    [BindProperty] public BaseQueryItemsParams QueryParams { get; set; } = new BaseQueryItemsParams();
    [BindProperty] public BaseQueryResult<TileAdViewModel> QueryResult { get; set; }

    [BindProperty]
    public List<TileAdViewModel>? TileAds =>
        QueryResult.Success ? QueryResult.Items as List<TileAdViewModel> : new List<TileAdViewModel>();

    public Favorites(
        AdsService adsService,
        BaseUrlOptions baseUrlOptions)
    {
        _adsService = adsService;
        BaseUrl = baseUrlOptions.GetFullApiUrl("Files");
    }

    public async Task<ActionResult> OnGetAsync()
    {
        QueryResult = await _adsService.GetFavoritesAds(QueryParams);
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