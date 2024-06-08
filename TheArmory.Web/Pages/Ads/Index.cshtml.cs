using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.Region;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Ads;

public class Index : PageModel
{
    private readonly RegionsService _regionsService;
    private readonly AdsService _adsService;
    public readonly string BaseUrl;
    
    [BindProperty] public BaseResult RequestResult { get; set; } = new BaseResult();
    
    [BindProperty] public List<RegionListViewModel> Regions { get; set; }

    [BindProperty] public TileAdQueryItemsParams QueryParams { get; set; } = new TileAdQueryItemsParams();
    
    [BindProperty] public AdFilterViewModel AdFilterViewModel { get; set; }
    
    [BindProperty] public BaseQueryResult<TileAdViewModel> QueryResult { get; set; }
    
    [BindProperty] public List<TileAdViewModel>? TileAds => QueryResult.Success ? QueryResult.Items as List<TileAdViewModel> : new List<TileAdViewModel>(); 

    public Index(
        AdsService adsService,
        ConditionsService conditionsService,
        RegionsService regionsService,
        BaseUrlOptions baseUrlOptions)
    {
        _adsService = adsService;
        _regionsService = regionsService;
        BaseUrl = baseUrlOptions.GetFullApiUrl("Files");
    }
    
    public async Task<ActionResult> OnGetAsync()
    {
        var adFilterViewModelResult = await _adsService.GetFilterViewModel();
        if (adFilterViewModelResult.Success)
            AdFilterViewModel = adFilterViewModelResult.Item;
        
        var regionsQueryResult = await _regionsService.GetSelectList();
        Regions = regionsQueryResult.Items.ToList();
        
        QueryResult = await _adsService.GetAds(QueryParams);
        if (!QueryResult.Success) RequestResult = QueryResult;
        return Page();
    }

    public async Task<ActionResult> OnPostFilterAsync()
    {
        return await OnGetAsync();
    }
}