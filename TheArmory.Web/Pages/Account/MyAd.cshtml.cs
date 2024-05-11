using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
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
    
    [BindProperty] public BaseResult Result { get; set; } = new BaseResult();
    [BindProperty] public AdPublishInfoViewModel PublishInfoViewModel { get; set; }
    
    [BindProperty] public AdUpdateCommand Command { get; set; }
    
    [BindProperty] public MyAdViewModel MyAdViewModel { get; set; }
    
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
    
    public async Task<ActionResult> OnGetSelectedAsync()
    {
        var adPublishInfoResult = await _adsService.GetPublishInformation();
        if (adPublishInfoResult.Success)
            PublishInfoViewModel = adPublishInfoResult.Item;
        
        var adResult = await _adsService.GetSelectedMy();
        if (adResult.Success)
            MyAdViewModel = adResult.Item;
        
        return Page();
    }
    
    public async Task<ActionResult> OnGetSelectAsync(Guid id)
    {
        var adPublishInfoResult = await _adsService.GetPublishInformation();
        if (adPublishInfoResult.Success)
            PublishInfoViewModel = adPublishInfoResult.Item;
        
        var adResult = await _adsService.SelectMy(new AdSelectCommand(){Id = id});
        if (adResult.Success)
            MyAdViewModel = adResult.Item;
        
        return Page();
    }

    public async Task<ActionResult> OnPostActivateAsync()
    {
        Result = await _adsService.ActivateAd();
        if (!Result.Success) return Page();
        return await OnGetSelectedAsync();
    }
    
    public async Task<ActionResult> OnPostDeactivateAsync()
    {
        Result = await _adsService.DeactivateAd();
        if (!Result.Success) return Page();
        return await OnGetSelectedAsync();
    }
}