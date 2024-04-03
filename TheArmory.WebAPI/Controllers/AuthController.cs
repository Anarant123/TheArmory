using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels;
using TheArmory.Repository;
using TheArmory.Utils;

namespace TheArmory.Controllers;


[AllowAnonymous]
[Route("Auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UsersRepository _usersRepository;
    private readonly ILogger<AuthController> _logger;
    
    public AuthController(UsersRepository usersRepository, ILogger<AuthController> logger)
    {
        _usersRepository = usersRepository;
        _logger = logger;
    }
    
    /// <summary>
    /// Удаление cookie текущей сессии
    /// </summary>
    [HttpGet]
    [Route("Logout")]
    public async Task Logout()
    {
        await HttpContext.SignOutAsync();
    }

    /// <summary>
    /// Авторизация
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<BaseResult<UserViewModel>>> Login(
        [FromBody]UserLoginCommand command)
    {
        var userResponse = await _usersRepository.Login(command);

        if (!userResponse.Success)
            return BadRequest(userResponse);
        
        await AuthUtils.SetLoginClaims(userResponse.Item, HttpContext, command?.RememberMe == true);

        return Ok(new BaseResult<UserViewModel>(userResponse.Item));
    }
    
    /// <summary>
    /// Регистрация
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<BaseResult>> Register(
        [FromBody]UserCreateCommand command)
    {
        var userResponse = await _usersRepository.Create(command);

        if (!userResponse.Success)
            return BadRequest(userResponse);
        
        return Ok(userResponse);
    }
}