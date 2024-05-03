using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Repository;

namespace TheArmory.Controllers;

public class AdminsController : ControllerBase
{
    private readonly UsersRepository _usersRepository;
    private readonly ILogger<AdminsController> _logger;
    
    public AdminsController(UsersRepository usersRepository, ILogger<AdminsController> logger)
    {
        _usersRepository = usersRepository;
        _logger = logger;
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
        [FromBody]UserCreateCommand command)
    {
        var userResponse = await _usersRepository.Create(command);

        if (!userResponse.Success)
            return BadRequest(userResponse);
        
        return Ok(userResponse);
    }
    
}