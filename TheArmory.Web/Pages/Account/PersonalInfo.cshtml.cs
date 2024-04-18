using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.User;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Account;

public class PersonalInfo : PageModel
{
    private readonly UserService _userService;
    private readonly ConditionsService _conditionsService;
    private readonly RegionsService _regionsService;
    private readonly AdsService _adsService;
    public readonly string BaseUrl;
    
    [BindProperty]
    public UserPersonalInfoViewModel User { get; set; }
    
    [BindProperty]
    public BaseQueryResult<TileAdViewModel> QueryResult { get; set; }
    
    [BindProperty]
    public List<TileAdViewModel>? TileAds => QueryResult.Success ? QueryResult.Items as List<TileAdViewModel> : new List<TileAdViewModel>(); 
    
    [BindProperty]
    public UserChangeProfilePhotoCommand ChangeProfilePhotoCommand { get; set; }


    public PersonalInfo(UserService userService, 
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
    
    public async Task OnGetAsync()
    {
        var userResponce = await _userService.GetMe();
        User = userResponce.Item;
        QueryResult = await _adsService.GetMyAds();
    }
    
    
    public async Task OnPostChangePhotoAsync()
    {
        await _userService.ChangePhoto(ChangeProfilePhotoCommand);
    }
}