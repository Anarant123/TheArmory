using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Region;
using TheArmory.Repository;

namespace TheArmory.Controllers;

[Route("Regions")]
[ApiController]
public class RegionsController : BaseController
{
    protected readonly ILogger<ConditionsController> Logger;
    private readonly RegionsRepository _regionsRepository;

    public RegionsController(
        ILogger<ConditionsController> logger,
        UsersRepository usersRepository,
        RegionsRepository regionsRepository)
        : base(logger, usersRepository)
    {
        Logger = logger;
        _regionsRepository = regionsRepository;
    }

    /// <summary>
    /// Получить список регионов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("SelectList")]
    public async Task<ActionResult<BaseQueryResult<RegionListViewModel>>> GetSelectList()
    {
        var result = await _regionsRepository.GetSelectList();
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
}