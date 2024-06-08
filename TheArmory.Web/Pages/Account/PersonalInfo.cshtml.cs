using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Request.Commands.Contact;
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
    private readonly AdsService _adsService;
    private readonly ContactsService _contactsService;
    private readonly AuthService _authService;

    public readonly string BaseUrl;

    [BindProperty] public UserPersonalInfoViewModel UserInfo { get; set; }

    [BindProperty]
    public BaseQueryResult<TileAdViewModel> ActiveAdsQueryResult { get; set; } = new BaseQueryResult<TileAdViewModel>();

    [BindProperty]
    public BaseQueryResult<TileAdViewModel> InactiveAdsQueryResult { get; set; } =
        new BaseQueryResult<TileAdViewModel>();

    [BindProperty]
    public BaseQueryResult<TileAdViewModel> BannedAdsQueryResult { get; set; } = new BaseQueryResult<TileAdViewModel>();

    [BindProperty] public BaseResult Result { get; set; } = new BaseResult();
    [BindProperty] public UserChangeProfilePhotoCommand ChangeProfilePhotoCommand { get; set; }
    [BindProperty] public UserChangeNameCommand ChangeNameCommand { get; set; }
    [BindProperty] public ContactCreateCommand ContactCreateCommand { get; set; }

    [BindProperty] public ContactCommand DeleteContactCommand { get; set; }

    public PersonalInfo(UserService userService,
        AdsService adsService,
        BaseUrlOptions baseUrlOptions,
        ContactsService contactsService,
        AuthService authService)
    {
        _userService = userService;
        _adsService = adsService;
        BaseUrl = baseUrlOptions.GetFullApiUrl("Files");
        _contactsService = contactsService;
        _authService = authService;
    }

    public async Task<ActionResult> OnGetAsync()
    {
        if (User.Identity is { IsAuthenticated: false })
            return RedirectToPage("/Auth/Index");

        var userResponce = await _userService.GetMe();
        UserInfo = userResponce.Item;


        ActiveAdsQueryResult = await _adsService.GetMyAds(new MyTileAdQueryItemsParams()
            { StatusId = StateStatus.Actively });
        InactiveAdsQueryResult = await _adsService.GetMyAds(new MyTileAdQueryItemsParams()
            { StatusId = StateStatus.Inactive });
        BannedAdsQueryResult =
            await _adsService.GetMyAds(new MyTileAdQueryItemsParams() { StatusId = StateStatus.Banned });
        return Page();
    }


    public async Task<IActionResult> OnPostChangePhotoAsync()
    {
        var BaseResult = await _userService.ChangePhoto(ChangeProfilePhotoCommand);
        if (!BaseResult.Success) return Page();
        await OnGetAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostChangeNameAsync()
    {
        Result = await _userService.ChangeName(ChangeNameCommand);
        if (!Result.Success) return Page();
        await OnGetAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostCreateContactAsync()
    {
        Result = await _contactsService.CreateContact(ContactCreateCommand);
        if (!Result.Success) return Page();
        await OnGetAsync();
        return Page();
    }


    public async Task<IActionResult> OnPostExitAsync()
    {
        Result = await _authService.Logout();
        if (!Result.Success) return await OnGetAsync();
        await HttpContext.SignOutAsync();
        return RedirectToPage("/Ads/Index");
    }

    public async Task<IActionResult> OnPostDeleteContactAsync()
    {
        Result = await _contactsService.DeleteContact(DeleteContactCommand);
        if (!Result.Success) return Page();
        await OnGetAsync();
        return Page();
    }
}