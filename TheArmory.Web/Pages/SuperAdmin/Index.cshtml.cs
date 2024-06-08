using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.User;
using TheArmory.Web.Models;
using TheArmory.Web.Service;

namespace TheArmory.Web.Pages.SuperAdmin;

public class Index : PageModel
{
    private readonly AdminsService _adminsService;
    public readonly string BaseUrl;

    [BindProperty] public UserCommand Command { get; set; }
    [BindProperty] public BaseResult Result { get; set; } = new BaseResult();
    [BindProperty] public BaseQueryItemsParams QueryParams { get; set; } = new BaseQueryItemsParams();
    [BindProperty] public BaseQueryResult<UserViewModel> QueryResult { get; set; }

    [BindProperty]
    public List<UserViewModel>? Admins =>
        QueryResult.Success ? QueryResult.Items as List<UserViewModel> : new List<UserViewModel>();

    public Index(
        AdminsService adminsService,
        BaseUrlOptions baseUrlOptions)
    {
        _adminsService = adminsService;
        BaseUrl = baseUrlOptions.GetFullApiUrl("Files");
    }

    public async Task<ActionResult> OnGetAsync()
    {
        QueryResult = await _adminsService.GetAdmins(QueryParams);
        if (!QueryResult.Success)
        {
        }

        return Page();
    }

    public async Task<ActionResult> OnPostAsync()
    {
        return await OnGetAsync();
    }

    public async Task<ActionResult> OnPostDeleteAsync()
    {
        Result = await _adminsService.Delete(Command);
        if (!Result.Success) return Page();
        return await OnGetAsync();
    }
}