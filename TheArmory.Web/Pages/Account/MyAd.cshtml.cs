using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.Condition;
using TheArmory.Domain.Models.Responce.ViewModels.Region;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Account;

public class MyAd : PageModel
{
    private readonly UserService _userService;
    private readonly ConditionsService _conditionsService;
    private readonly RegionsService _regionsService;
    private readonly AdsService _adsService;
    public readonly string BaseUrl;
    
    [BindProperty]
    public List<ConditionListViewModel> Conditions { get; set; }
    
    [BindProperty]
    public List<RegionListViewModel> Regions { get; set; }
    
    public MyAdViewModel MyAdViewModel { get; set; }
    
    public MyAd(UserService userService, 
        AdsService adsService,
        ConditionsService conditionsService,
        RegionsService regionsService,
        BaseUrlOptions baseUrlOptions)
    {
        _userService = userService;
        _adsService = adsService;
        _conditionsService = conditionsService;
        _regionsService = regionsService;
        BaseUrl = baseUrlOptions.GetFullApiUrl("Files");
    }
    
    public async Task OnGet(Guid id)
    {
        var adResult = await _adsService.GetMyAd(id);
        if (adResult.Success)
            MyAdViewModel = adResult.Item;
        
        var conditionsQueryResult = await _conditionsService.GetSelectList();
        if (conditionsQueryResult.Success)
            Conditions = conditionsQueryResult.Items.ToList();
        
        var regionsQueryResult = await _regionsService.GetSelectList();
        if (regionsQueryResult.Success)
            Regions = regionsQueryResult.Items.ToList();
    }
}