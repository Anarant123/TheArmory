using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.Complaint;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.Ads;

public class AdInfo : PageModel
{
    private readonly AdsService _adsService;
    private readonly ComplaintsService _complaintsService;
    public readonly string BaseUrl;

    [BindProperty] public BaseResult RequestResult { get; set; } = new BaseResult();

    [BindProperty] public AdToComplaintCommand ToComplaintCommand { get; set; }

    [BindProperty] public BaseQueryResult<ComplaintViewModel> Complaints { get; set; }

    [BindProperty] public AdViewModel AdViewModel { get; set; }

    public AdInfo(AdsService adsService,
        ComplaintsService complaintsService,
        BaseUrlOptions baseUrlOptions)
    {
        _adsService = adsService;
        _complaintsService = complaintsService;
        BaseUrl = baseUrlOptions.GetFullApiUrl("Files");
    }

    public async Task<ActionResult> OnGetSelectAsync(Guid id)
    {
        var result = await _adsService.Select(new AdSelectCommand() { Id = id });
        if (result.Success)
            AdViewModel = result.Item;

        if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Admin")
            Complaints = await _complaintsService.GetComplaints();

        return Page();
    }

    public async Task<ActionResult> OnGetSelectedAsync()
    {
        var result = await _adsService.GetSelected();
        if (result.Success)
            AdViewModel = result.Item;

        if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Admin")
            Complaints = await _complaintsService.GetComplaints();

        return Page();
    }

    public async Task<ActionResult> OnPostAddToFavoritesAsync()
    {
        if (User.Identity is { IsAuthenticated: false })
        {
            RequestResult = new BaseResult("Чтобы добавить в избранное это объявление вам необходимо авторизоваться");
            return await OnGetSelectedAsync();
        }

        var result = await _adsService.ToFavorites();
        if (!result.Success)
            RequestResult = result;

        return await OnGetSelectedAsync();
    }

    public async Task<ActionResult> OnPostDeleteFavoritesAsync()
    {
        var result = await _adsService.DeleteFavorite();
        if (!result.Success)
            RequestResult = result;

        return await OnGetSelectedAsync();
    }

    public async Task<ActionResult> OnPostComplainAsync()
    {
        if (User.Identity is { IsAuthenticated: false })
        {
            RequestResult = new BaseResult("Чтобы оставить жалобу на данное объявление вам необходимо авторизоваться");
            return await OnGetSelectedAsync();
        }

        var result = await _adsService.Complaint(ToComplaintCommand);
        if (!result.Success)
            RequestResult = result;

        return await OnGetSelectedAsync();
    }

    public async Task<ActionResult> OnPostBanAsync()
    {
        var result = await _adsService.Ban();
        if (!result.Success)
        {
            RequestResult = result;
            return await OnGetSelectedAsync();
        }

        return RedirectToPage("/Admin/Index");
    }

    public async Task<ActionResult> OnPostJustifyAsync()
    {
        var result = await _adsService.Justify();
        if (!result.Success)
        {
            RequestResult = result;
            return await OnGetSelectedAsync();
        }

        return RedirectToPage("/Admin/Index");
    }
}