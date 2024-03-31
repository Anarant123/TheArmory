using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheArmory.Domain.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels;

namespace TheArmory.Repository;

public class UsersRepository : BaseRepository
{
    private readonly PasswordHasher<User> _passwordHasher;
    public UsersRepository(
        ApplicationContext context,
        ILogger<BaseRepository<User>> logger,
        PasswordHasher<User> passwordHasher)
        : base(context, logger)
    {
        _passwordHasher = passwordHasher;
    }
    
    // todo Авторизация
    public async Task<BaseResult<UserViewModel>> Login(
        UserLoginCommand command)
    {
        if (command is not ({ Login: not null } and { Password: not null }))
            return new BaseResult<UserViewModel>(ErrorsMessage.SomethingWentWrong);

        var user = await Context.Users
            .FirstOrDefaultAsync(u => u.Email.Equals(command.Login) || u.PhoneNumber.Equals(command.Login));

        if (user is null || user.StatusId.Equals(StateStatus.Deleted))
            return new BaseResult<UserViewModel>(ErrorsMessage.UserNotFound);

        return _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password) switch
        {
            PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded =>
                new BaseResult<UserViewModel>(new UserViewModel(user)),
            _ => new BaseResult<UserViewModel>(ErrorsMessage.InvalidPassword)
        };
    }
    
    // todo Регистрация
     public async Task<BaseResult> Create(
        UserCreateCommand command)
    {
        if (command is not ({ Name: not null }
            and { PhoneNumber: not null}
            and { Email: not null }
            and { Password: not null }
            and { PasswordConfirm: not null }))
            return new BaseResult(ErrorsMessage.SomethingWentWrong);

        if (!command.Password.Equals(command.PasswordConfirm))
            return new BaseResult(ErrorsMessage.ConfirmPasswordNotMatch);

        if (await Context.Users.AnyAsync(p => p.Email.Equals(command.Email))!)
            return new BaseResult(ErrorsMessage.InaccessibleEmail);

        var role = await Context.Roles.FirstAsync(r => r.Id == UserRole.Client);
        
        var user = new User()
        {
            Name = command.Name,
            PhoneNumber = command.PhoneNumber,
            Email = command.Email,
            Role = role,
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
    
    // todo Получение своего профиля
    public async Task<BaseResult<UserViewModel>> Get(
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
    
    // todo Изменение своего профиля
    
    // todo Удаление своего профиля
    
    // todo Смена пароля
    
}