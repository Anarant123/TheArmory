using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;

namespace TheArmory.Web.Pages.Ads;

public class Index : PageModel
{
    public List<TileAdViewModel> TileAds { get; set; }
    
    public void OnGet()
    {
        
    }
}