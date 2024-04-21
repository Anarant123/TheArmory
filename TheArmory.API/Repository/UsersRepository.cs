using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels;
using TheArmory.Domain.Models.Responce.ViewModels.User;

namespace TheArmory.Repository;

public class UsersRepository : BaseRepository
{
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly MediasRepository _mediasRepository;
    public UsersRepository(
        ApplicationContext context,
        ILogger<BaseRepository<User>> logger,
        PasswordHasher<User> passwordHasher,
        MediasRepository mediasRepository)
        : base(context, logger)
    {
        _passwordHasher = passwordHasher;
        _mediasRepository = mediasRepository;
    }
    
    // todo Авторизация
    public async Task<BaseResult<UserViewModel>> Login(
        UserLoginCommand command)
    {
        if (command is not ({ Login: not null } and { Password: not null }))
            return new BaseResult<UserViewModel>(ErrorsMessage.SomethingWentWrong);

        var user = await Context.Users
            .FirstOrDefaultAsync(u => u.Login.Equals(command.Login));

        if (user is null || user.StatusId.Equals(StateStatus.Deleted))
            return new BaseResult<UserViewModel>(ErrorsMessage.UserNotFound);

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password);

        if (result is not (PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded))
        {
            return new BaseResult<UserViewModel>(ErrorsMessage.InvalidPassword);
        }
        
        user.LastVisitDate = DateTime.Now;
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult<UserViewModel>(ErrorsMessage.ErrorSavingChanges),
            _ => new BaseResult<UserViewModel>(new UserViewModel(user))
        };
    }
    
    // todo Регистрация
     public async Task<BaseResult> Create(
        UserCreateCommand command)
    {
        if (command is not ({ Name: not null }
            and { Login: not null }
            and { Password: not null }
            and { PasswordConfirm: not null }))
            return new BaseResult(ErrorsMessage.SomethingWentWrong);

        if (!command.Password.Equals(command.PasswordConfirm))
            return new BaseResult(ErrorsMessage.ConfirmPasswordNotMatch);

        if (await Context.Users.AnyAsync(p => p.Login.Equals(command.Login))!)
            return new BaseResult(ErrorsMessage.InaccessibleEmail);
        
        
        var user = new User()
        {
            Name = command.Name,
            Login = command.Login,
            RoleId = UserRole.Client,
            StatusId = StateStatus.Actively,
            RegistrationDateTime = DateTime.Now,
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, command.Password);

        await Context.Users.AddAsync(user);

        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult(ErrorsMessage.ErrorSavingChanges),
            _ => new BaseResult()
        };
    }
    
    
    /// <summary>
    /// Пользователь по Id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<BaseResult<User>> Get(
        Guid userId)
    {
        var user = await Context.Users
            .Include(u => u.Region)
            .Include(u => u.Status)
            .FirstOrDefaultAsync(u => u.Id.Equals(userId));
        if (user is null)
            return new BaseResult<User>(ErrorsMessage.UserNotFound);
        user.LastVisitDate = DateTime.Now;
        
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult<User>(ErrorsMessage.ErrorSavingChanges),
            _ => new BaseResult<User>(user)
        };
    }
    
    // todo Изменение своего профиля
    public async Task<BaseResult<UserViewModel>> Update(
        Guid userId)
    {
        var user = await Context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
        if (user is null)
            return new BaseResult<UserViewModel>(ErrorsMessage.UserNotFound);
        user.LastVisitDate = DateTime.Now;
        
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult<UserViewModel>(ErrorsMessage.ErrorSavingChanges),
            _ => new BaseResult<UserViewModel>(new UserViewModel(user))
        };
    }
    
    // todo Удаление своего профиля
    
    // todo Смена пароля
    
    
    public async Task<BaseResult> ChangeProfilePhoto(
        User user,
        IFormFile photo)
    {
        var changeResult = await _mediasRepository.ChangeProfilePhoto(user.Id, photo);
        if (!changeResult.Success)
            return new BaseResult("Сменить фото профиля не удалось");

        user.PhotoName = changeResult.Item;
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult(ErrorsMessage.ErrorSavingChanges),
            _ => new BaseResult()
        };
    }
}