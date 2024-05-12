using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.Condition;
using TheArmory.Domain.Models.Responce.ViewModels.Region;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Publication;

public class Index : PageModel
{
    private readonly AdsService _adsService;
    private readonly ConditionsService _conditionsService;
    private readonly RegionsService _regionsService;
    
    [BindProperty] public AdPublishInfoViewModel PublishInfoViewModel { get; set; }
    
    [BindProperty] public AdCreateCommand Command { get; set; }

    public Index(AdsService adsService, ConditionsService conditionsService, RegionsService regionsService)
    {
        _adsService = adsService;
        _conditionsService = conditionsService;
        _regionsService = regionsService;
    }
    
    public async Task OnGetAsync()
    {
        var adPublishInfoResult = await _adsService.GetPublishInformation();
        if (adPublishInfoResult.Success)
            PublishInfoViewModel = adPublishInfoResult.Item;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Categories");
        ModelState.Remove("YouTubeLink");
        
        if (Command.CategoryId != Guid.Parse("3666fab5-1175-4e6c-b9ac-ff9b811e7d8a"))
        {
            ModelState.Remove("WeaponTypes");
            ModelState.Remove("Calibers");
            ModelState.Remove("BarrelPositions");
            ModelState.Remove("YearOfProduction");
            ModelState.Remove("Conditions");
        }
        
        var result = await _adsService.PostAd(Command);

        if (result.Success)
        {
            return RedirectToPage("/Account/MyAd");
        }

        await OnGetAsync();
        return Page();
    }
}