using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Complaint;
using TheArmory.Repository;

namespace TheArmory.Controllers;

[Authorize]
[Route("Complaints")]
[ApiController]
public class ComplaintsController : BaseController
{
    protected readonly ILogger<ComplaintsController> Logger;
    private readonly ComplaintsRepository _complaintsRepository;

    public ComplaintsController(
        ILogger<ComplaintsController> logger,
        UsersRepository usersRepository,
        ComplaintsRepository complaintsRepository)
        : base(logger, usersRepository)
    {
        Logger = logger;
        _complaintsRepository = complaintsRepository;
    }

    /// <summary>
    /// Получение жалоб выбранного объявления 
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<BaseQueryResult<ComplaintViewModel>>> Get(
        [FromQuery] BaseQueryItemsParams queryItemsParams)
    {
        var adIdResponse = GetSelectedAdId();
        if (!adIdResponse.Success)
        {
            adIdResponse = GetSelectedMyAdId();
            if (!adIdResponse.Success)
                return BadRequest(adIdResponse);
        }

        var result = await _complaintsRepository.Get(adIdResponse.Item, queryItemsParams);

        if (result.Success) return Ok(result);
        Logger.LogError(result.Error);
        return BadRequest(result);
    }
}