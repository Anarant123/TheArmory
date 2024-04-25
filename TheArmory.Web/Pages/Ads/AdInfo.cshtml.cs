using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.User;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Ads;

public class AdInfo : PageModel
{
    private readonly AdsService _adsService;
    public readonly string BaseUrl;
    
    [BindProperty]
    public AdToComplaintCommand ToComplaintCommand { get; set; }
    
    [BindProperty]
    public AdViewModel AdViewModel { get; set; }
    
    public AdInfo(AdsService adsService, BaseUrlOptions baseUrlOptions)
    {
        _adsService = adsService;
        BaseUrl = baseUrlOptions.GetFullApiUrl("Files");
    }

    public async Task OnGetSelectAsync(Guid id)
    {
        var result = await _adsService.Select(new AdSelectCommand(){Id = id});
        if (result.Success)
            AdViewModel = result.Item;
        
        //todo написать возвращение страницы ошибки
    }
    
    public async Task OnGetSelectedAsync()
    {
        var result = await _adsService.GetSelected();
        if (result.Success)
            AdViewModel = result.Item;
        
        //todo написать возвращение страницы ошибки
    }

    public async Task OnPostAddToFavoritesAsync()
    {
        var result = await _adsService.ToFavorites();
        if (result.Success)
            return;
    }
    
    public async Task OnPostComplainAsync()
    {
        var result = await _adsService.Complaint(ToComplaintCommand);
        if (result.Success)
            return;
    }
}