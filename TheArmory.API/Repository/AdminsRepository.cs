using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.User;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.User;

namespace TheArmory.Repository;

public class AdminsRepository : BaseRepository<User>
{
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly MediasRepository _mediasRepository;
    public AdminsRepository(
        ApplicationContext context,
        ILogger<BaseRepository<User>> logger,
        PasswordHasher<User> passwordHasher,
        MediasRepository mediasRepository)
        : base(context, logger)
    {
        _passwordHasher = passwordHasher;
        _mediasRepository = mediasRepository;
    }

    public async Task<BaseQueryResult<UserViewModel>> Get(
        BaseQueryItemsParams queryItemsParams)
    {
        var admins = await Context.Users
            .Where(u => u.RoleId.Equals(UserRole.Admin) 
                        && u.StatusId.Equals(StateStatus.Actively))
            .Select(s => new UserViewModel(s))
            .ToListAsync();

        return new BaseQueryResult<UserViewModel>(admins);
    }
    
    /// <summary>
    /// Создает администратора
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<BaseResult> Create(
        UserAdminCreateCommand command)
    {
        if (command is not ({ Name: not null }
            and { Login: not null }
            and { Password: not null }
            and { PasswordConfirm: not null }))
            return new BaseResult(ErrorsMessage.SomethingWentWrong);

        if (!command.Password.Equals(command.PasswordConfirm))
            return new BaseResult(ErrorsMessage.ConfirmPasswordNotMatch);

        if (await Context.Users.AnyAsync(p => p.Login.Equals(command.Login))!)
            return new BaseResult(ErrorsMessage.InaccessibleLogin);
        
        
        var user = new User()
        {
            Login = command.Login,
            Name = command.Name,
            RoleId = UserRole.Admin,
            StatusId = StateStatus.Actively,
            RegistrationDateTime = DateTime.Now,
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, command.Password);
        var changeResult = await _mediasRepository.ChangeProfilePhoto(user.Id, command.Photo);
        if (!changeResult.Success)
            return new BaseResult("Сменить фото профиля не удалось");

        user.PhotoName = changeResult.Item;
        await Context.Users.AddAsync(user);

        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult(ErrorsMessage.ErrorSavingChanges),
            _ => new BaseResult()
        };
    }

    public async Task<BaseResult> Delete(
        UserCommand command)
    {
        var admin = await Context.Users
            .FirstOrDefaultAsync(u => u.Id.Equals(command.Id)
                                      && u.RoleId.Equals(UserRole.Admin));

        if (admin is null) return new BaseResult("Админ не найден");

        admin.StatusId = StateStatus.Deleted;
        
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult(ErrorsMessage.ErrorSavingChanges),
            _ => new BaseResult()
        };
    }
}