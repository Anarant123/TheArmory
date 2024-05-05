using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.User;
using TheArmory.Repository;

namespace TheArmory.Controllers;

[AllowAnonymous]
[Route("Admins")]
[ApiController]
public class AdminsController : ControllerBase
{
    private readonly AdminsRepository _adminsRepository;
    private readonly ILogger<AdminsController> _logger;
    
    public AdminsController(AdminsRepository adminsRepository, ILogger<AdminsController> logger)
    {
        _adminsRepository = adminsRepository;
        _logger = logger;
    }

    /// <summary>
    /// Администраторы
    /// </summary>
    /// <param name="queryItemsParams"></param>
    /// <returns></returns>
    [Authorize(Roles = "SuperAdmin")]
    [HttpGet]
    [Route("Admins")]
    public async Task<ActionResult<BaseQueryResult<UserViewModel>>> Get(
        [FromQuery]BaseQueryItemsParams queryItemsParams )
    {
        var userResponse = await _adminsRepository.Get(queryItemsParams);

        if (!userResponse.Success)
            return BadRequest(userResponse);
        
        return Ok(userResponse);
    }
    
    /// <summary>
    /// Регистрация
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Authorize(Roles = "SuperAdmin")]
    [HttpPost]
    [Route("Registration")]
    public async Task<ActionResult<BaseResult>> Register(
        [FromForm]UserAdminCreateCommand command)
    {
        var userResponse = await _adminsRepository.Create(command);

        if (!userResponse.Success)
            return BadRequest(userResponse);
        
        return Ok(userResponse);
    }
}