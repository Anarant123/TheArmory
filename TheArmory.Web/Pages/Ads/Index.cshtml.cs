using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.Condition;
using TheArmory.Domain.Models.Responce.ViewModels.Region;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Ads;

public class Index : PageModel
{
    private readonly ConditionsService _conditionsService;
    private readonly RegionsService _regionsService;
    private readonly AdsService _adsService;
    public readonly string BaseUrl;
    
    [BindProperty]
    public List<ConditionListViewModel> Conditions { get; set; }
    
    [BindProperty]
    public List<RegionListViewModel> Regions { get; set; }
    
    [BindProperty]
    public TileAdQueryItemsParams QueryParams { get; set; }
    
    [BindProperty]
    public BaseQueryResult<TileAdViewModel> QueryResult { get; set; }
    
    [BindProperty]
    public List<TileAdViewModel>? TileAds => QueryResult.Success ? QueryResult.Items as List<TileAdViewModel> : new List<TileAdViewModel>(); 

    public Index(
        AdsService adsService,
        ConditionsService conditionsService,
        RegionsService regionsService,
        BaseUrlOptions baseUrlOptions)
    {
        _adsService = adsService;
        _conditionsService = conditionsService;
        _regionsService = regionsService;
        BaseUrl = baseUrlOptions.GetFullApiUrl("Files");
    }
    
    public async Task OnGet()
    {
        // var conditionsQueryResult = await _conditionsService.GetSelectList();
        // Conditions = conditionsQueryResult.Items.ToList();
        
        var regionsQueryResult = await _regionsService.GetSelectList();
        Regions = regionsQueryResult.Items.ToList();
        
        QueryResult = await _adsService.GetAds();
    }
   
}