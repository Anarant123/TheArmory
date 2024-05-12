using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Request.Commands.Characteristic;
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
    private readonly AdsService _adsService;
    private readonly CharacteristicsService _characteristicsService;
    public readonly string BaseUrl;
    
    [BindProperty] public BaseResult Result { get; set; } = new BaseResult();
    [BindProperty] public AdPublishInfoViewModel PublishInfoViewModel { get; set; }
    
    [BindProperty] public AdUpdateCommand UpdateCommand { get; set; }
    [BindProperty] public CharacteristicCreateCommand CharacteristicCreateCommand { get; set; }
    [BindProperty] public CharacteristicCommand DeleteCharacteristicCommand { get; set; }
    
    [BindProperty] public MyAdViewModel MyAdViewModel { get; set; }
    
    public MyAd(UserService userService, 
        AdsService adsService,
        ConditionsService conditionsService,
        CharacteristicsService characteristicsService,
        BaseUrlOptions baseUrlOptions)
    {
        _userService = userService;
        _adsService = adsService;
        _conditionsService = conditionsService;
        _characteristicsService = characteristicsService;
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
    
    public async Task<ActionResult> OnPostDeleteAsync()
    {
        Result = await _adsService.DeleteAd();
        if (!Result.Success) return Page();
        return RedirectToPage("/Account/PersonalInfo");
    }

    public async Task<ActionResult> OnPostUpdateAsync()
    {
        Result = await _adsService.Update(UpdateCommand);
        if (!Result.Success) return Page();
        return await OnGetSelectedAsync();
    }
    
    public async Task<ActionResult> OnPostCreateCharacteristicAsync()
    {
        Result = await _characteristicsService.CreateCharacteristic(CharacteristicCreateCommand);
        if (!Result.Success) return Page();
        await OnGetSelectedAsync();
        return Page();
    }
    
    public async Task<ActionResult> OnPostDeleteCharacteristicAsync()
    {
        Result = await _characteristicsService.DeleteCharacteristic(DeleteCharacteristicCommand);
        if (!Result.Success) return Page();
        await OnGetSelectedAsync();
        return Page();
    }
}