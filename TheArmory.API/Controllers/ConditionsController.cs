using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Condition;
using TheArmory.Repository;

namespace TheArmory.Controllers;

[Route("Conditions")]
[ApiController]
public class ConditionsController : BaseController
{
    protected readonly ILogger<ConditionsController> Logger;
    private readonly ConditionsRepository _conditionsRepository;
    
    public ConditionsController(
        ILogger<ConditionsController> logger,
        UsersRepository usersRepository,
        ConditionsRepository conditionsRepository) 
        : base(logger, usersRepository)
    {
        Logger = logger;
        _conditionsRepository = conditionsRepository;
    }
    
    /// <summary>
    /// Получить себя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("SelectList")]
    public async Task<ActionResult<BaseQueryResult<ConditionListViewModel>>> GetSelectList()
    {
        var result = await _conditionsRepository.GetSelectList();
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
}