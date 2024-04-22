using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Request.Queries;
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
    private readonly ContactsService _contactsService;
    public readonly string BaseUrl;
    
    [BindProperty]
    public UserPersonalInfoViewModel User { get; set; }

    [BindProperty]
    public BaseQueryResult<TileAdViewModel> ActiveAdsQueryResult { get; set; } = new BaseQueryResult<TileAdViewModel>();
    
    [BindProperty]
    public BaseQueryResult<TileAdViewModel> InactiveAdsQueryResult { get; set; } = new BaseQueryResult<TileAdViewModel>();
    
    [BindProperty]
    public BaseQueryResult<TileAdViewModel> BannedAdsQueryResult { get; set; } = new BaseQueryResult<TileAdViewModel>();
     
    
    [BindProperty]
    public UserChangeProfilePhotoCommand ChangeProfilePhotoCommand { get; set; }
    
    [BindProperty]
    public UserChangeNameCommand ChangeNameCommand { get; set; }
    
    [BindProperty]
    public ContactCreateCommand ContactCreateCommand { get; set; }


    public PersonalInfo(UserService userService, 
        AdsService adsService,
        ConditionsService conditionsService,
        RegionsService regionsService,
        BaseUrlOptions baseUrlOptions,
        ContactsService contactsService)
    {
        _userService = userService;
        _adsService = adsService;
        _conditionsService = conditionsService;
        _regionsService = regionsService;
        BaseUrl = baseUrlOptions.GetFullApiUrl("Files");
        _contactsService = contactsService;
    }
    
    public async Task OnGetAsync()
    {
        var userResponce = await _userService.GetMe();
        User = userResponce.Item;
        
        
        ActiveAdsQueryResult = await _adsService.GetMyAds(new TileAdQueryItemsParams(){ StatusId = StateStatus.Actively});
        InactiveAdsQueryResult = await _adsService.GetMyAds(new TileAdQueryItemsParams(){ StatusId = StateStatus.Inactive});
        BannedAdsQueryResult = await _adsService.GetMyAds(new TileAdQueryItemsParams(){ StatusId = StateStatus.Banned});
    }
    
    
    public async Task OnPostChangePhotoAsync()
    {
        var result = await _userService.ChangePhoto(ChangeProfilePhotoCommand);
        if (result.Success)
        {
            await OnGetAsync();
        }
    }

    public async Task OnPostChangeNameAsync()
    {
        var result = await _userService.ChangeName(ChangeNameCommand);
        if (result.Success)
        {
            await OnGetAsync();
        }
    }

    public async Task OnPostCreateContact()
    {
        var result = await _contactsService.CreateContact(ContactCreateCommand);
        if (result.Success)
        {
            await OnGetAsync();
        }
    }

}