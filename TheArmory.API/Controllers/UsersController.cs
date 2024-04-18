using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels;
using TheArmory.Domain.Models.Responce.ViewModels.User;
using TheArmory.Repository;

namespace TheArmory.Controllers;

[Route("Users")]
[ApiController]
public class UsersController : BaseController
{
    protected readonly ILogger<UsersController> Logger;
    private readonly UsersRepository _usersRepository;
    
    public UsersController(ILogger<UsersController> logger, UsersRepository usersRepository) : base(logger, usersRepository)
    {
        Logger = logger;
        _usersRepository = usersRepository;
    }
    
    /// <summary>
    /// Получить себя
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("Me")]
    public async Task<ActionResult<BaseResult<UserPersonalInfoViewModel>>> GetMe()
    {
        var userResponse = await GetUser();
        return userResponse.Success switch
        {
            false => BadRequest(userResponse),
            true => Ok(new BaseResult<UserPersonalInfoViewModel>(new UserPersonalInfoViewModel(userResponse.Item)))
        };
    }

    /// <summary>
    /// Смена фотографии профиля
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>w
    [HttpPost]
    [Route("ChangeProfilePhoto")]
    public async Task<ActionResult<BaseResult>> ChangeProfilePhoto(
        [FromForm]UserChangeProfilePhotoCommand command)
    {
        var userResponse = await GetUser();
        if (userResponse is { Success: false, Item: not null })
            BadRequest(userResponse);

        var user = userResponse.Item!;

        var result = await _usersRepository.ChangeProfilePhoto(user, command.Photo);

        if (!result.Success)
            return BadRequest(result);
        
        return Ok(result);
    }
}