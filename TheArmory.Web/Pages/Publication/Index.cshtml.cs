using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Condition;
using TheArmory.Domain.Models.Responce.ViewModels.Region;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Publication;

public class Index : PageModel
{
    private readonly AdsService _adsService;
    private readonly ConditionsService _conditionsService;
    private readonly RegionsService _regionsService;
    
    public BaseQueryResult<ConditionListViewModel> ConditionsQueryResult { get; set; }
    public BaseQueryResult<RegionListViewModel> RegionsQueryResult { get; set; }
    
    [BindProperty]
    public List<ConditionListViewModel> Conditions { get; set; }
    
    [BindProperty]
    public List<RegionListViewModel> Regions { get; set; }
    
    [BindProperty]
    public AdCreateCommand Command { get; set; }

    public Index(AdsService adsService, ConditionsService conditionsService, RegionsService regionsService)
    {
        _adsService = adsService;
        _conditionsService = conditionsService;
        _regionsService = regionsService;
    }
    
    public async Task OnGetAsync()
    {
        ConditionsQueryResult = await _conditionsService.GetSelectList();
        Conditions = ConditionsQueryResult.Items.ToList();
        
        RegionsQueryResult = await _regionsService.GetSelectList();
        Regions = RegionsQueryResult.Items.ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var result = await _adsService.PostAd(Command);
        
        if (result.Success)
        {
            return RedirectToPage("/Ads/My");
        }

        ModelState.AddModelError(string.Empty, result.Error);
        return Page();
    }
}