using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Repository;

namespace TheArmory.Controllers;

public class BaseController : ControllerBase
{
    protected readonly ILogger<BaseController> Logger;

    private readonly UsersRepository _usersRepository;

    public BaseController(ILogger<BaseController> logger, UsersRepository usersRepository)
    {
        Logger = logger;
        _usersRepository = usersRepository;
    }

    protected async Task<BaseResult<User?>> GetUser()
    {
        if (User.Identity is null or { IsAuthenticated: false })
            return new BaseResult<User?>("Пользователь не аутентифицирован.");

        if (string.IsNullOrEmpty(User.Identity.Name))
            return new BaseResult<User?>("Произошла ошибка");

        var value = User.FindFirst("Id")?.Value;
        
        if (value == null) 
            return new BaseResult<User?>("Пользователь не аутентифицирован.");
        
        var userResponse = await _usersRepository
            .Get(Guid.Parse(value));

        return !userResponse.Success ? 
            new BaseResult<User?>(ErrorsMessage.UserNotFound) 
            : new BaseResult<User?>(userResponse.Item);

    }
}