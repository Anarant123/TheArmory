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

        var value = User.FindFirst("Id")?.Value;
        
        if (value == null) 
            return new BaseResult<User?>("Пользователь не аутентифицирован.");
        
        var userResponse = await _usersRepository
            .Get(Guid.Parse(value));

        return !userResponse.Success ? 
            new BaseResult<User?>(ErrorsMessage.UserNotFound) 
            : new BaseResult<User?>(userResponse.Item);

    }
    
    protected virtual BaseResult<Guid> GetSelectedAdId()
    {
        var selectedPhoneIdString = HttpContext.Session.GetString("SelectedAd") ?? string.Empty;
        var adId = !string.IsNullOrEmpty(selectedPhoneIdString)
            ? new Guid(selectedPhoneIdString)
            : Guid.Empty;
        return adId == Guid.Empty ? 
            new BaseResult<Guid>("Объявление не выбрано") 
            : new BaseResult<Guid>(adId);
    }
    
    protected virtual BaseResult<Guid> GetSelectedAdId(Guid adId)
    {
        if (adId != Guid.Empty) return new BaseResult<Guid>(adId);
        
        var selectedPhoneIdString = HttpContext.Session.GetString("netset") ?? string.Empty;
        adId = !string.IsNullOrEmpty(selectedPhoneIdString)
            ? new Guid(selectedPhoneIdString)
            : Guid.Empty;
        
        return adId == Guid.Empty ? 
            new BaseResult<Guid>("Объявление не выбрано") 
            : new BaseResult<Guid>(adId);
    }
}