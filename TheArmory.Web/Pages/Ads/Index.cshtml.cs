using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Ads;

public class Index : PageModel
{
    private readonly AdsService _adsService;
    public readonly string BaseUrl;
    
    public BaseQueryResult<TileAdViewModel> QueryResult { get; set; }
    
    [BindProperty]
    public List<TileAdViewModel> TileAds { get; set; }

    public Index(AdsService adsService, BaseUrlOptions baseUrlOptions)
    {
        _adsService = adsService;
        //BaseUrl = baseUrlOptions.GetFullApiUrl("");
    }
    
    public async Task OnGet()
    {
        QueryResult = await _adsService.GetAds();
        TileAds = (List<TileAdViewModel>)QueryResult.Items;
    }
}