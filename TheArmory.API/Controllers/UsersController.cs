using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels;
using TheArmory.Domain.Models.Responce.ViewModels.User;
using TheArmory.Repository;

namespace TheArmory.Controllers;

[Route("User")]
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
    public async Task<ActionResult<BaseResult<UserViewModel>>> GetMe()
    {
        var userResponse = await GetUser();
        return userResponse.Success switch
        {
            false => BadRequest(userResponse),
            true => Ok(userResponse)
        };
    }
}