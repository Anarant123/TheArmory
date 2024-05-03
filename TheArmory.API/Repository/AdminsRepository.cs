﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.User;
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
    
    /// <summary>
    /// Создает администратора
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<BaseResult> Create(
        UserAdminCreateCommand command)
    {
        if (command is not ({ Login: not null }
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