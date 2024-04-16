using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Ads;

public class AdInfo : PageModel
{
    private readonly AdsService _adsService;
    public readonly string BaseUrl;
    
    
    public AdViewModel AdViewModel { get; set; }
    
    public AdInfo(AdsService adsService, BaseUrlOptions baseUrlOptions)
    {
        _adsService = adsService;
        BaseUrl = baseUrlOptions.GetFullApiUrl("Files");
    }

    public async Task OnGetAsync(Guid id)
    {
        var result = await _adsService.GetAd(id);
        AdViewModel = result.Item;
    }

    public async Task OnPostAddToFavoritesAsync()
    {
        var g = 3 + 5;
    }
    
    public async Task OnPostComplainAsync()
    {
        var g = 3 + 5;
    }
}