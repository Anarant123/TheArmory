using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels;

namespace TheArmory.Repository;

public class AdsRepository : BaseRepository
{
    public AdsRepository(ApplicationContext context, ILogger<BaseRepository<Ad>> logger)
        : base(context, logger)
    {
    }

    // todo получить свое объявление
    
    
    // todo получить объявление
    
    // todo получить свои объявления
    
    // todo получить все объявления
    
    // todo фоток нема
    /// <summary>
    /// Создание объявления
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<BaseResult<AdViewModel>> Create(
        Guid userId,
        AdCreateCommand command)
    {
        var newAd = new Ad()
        {
            Name = command.Name,
            Price = command.Price,
            Description = command.Description ?? "",
            ConditionId = command.ConditionId,
            RegionId = command.RegionId,
            UserId = userId,
        };

        Context.Ads.Add(newAd);
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult<AdViewModel>("Произошла ошибка при сохранении данных"),
            _ => new BaseResult<AdViewModel>(new AdViewModel(newAd))
        };
    }

    /// <summary>
    /// Обновление объявления
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<BaseResult<AdViewModel>> Update(
        Guid userId,
        AdUpdateCommand command)
    {
        var ad = await Context.Ads.FirstOrDefaultAsync(a => a.Id.Equals(command.Id) && a.UserId.Equals(userId));
        if (ad is null)
            return new BaseResult<AdViewModel>("Объявление не найдено");

        if (!string.IsNullOrEmpty(command.Name))
            ad.Name = command.Name;
        if (command.Price is not null)
        {
            ad.OldPrice = ad.Price;
            ad.Price = Convert.ToDecimal(command.Price);
        }
        if (!string.IsNullOrEmpty(command.Description))
            ad.Description = command.Description;
        if (!string.IsNullOrEmpty(command.YouToubeLink))
            ad.YouToubeLink = command.YouToubeLink;
        if (command.ConditionId is not null)
            ad.ConditionId = (WeaponCondition)command.ConditionId;
        if (command.RegionId is not null)
            ad.RegionId = (Guid)command.RegionId;
        
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult<AdViewModel>("Произошла ошибка при сохранении данных"),
            _ => new BaseResult<AdViewModel>(new AdViewModel(ad))
        };
    }

    /// <summary>
    /// Удаление объявления
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<BaseResult> Delete(
        Guid userId,
        AdDeleteCommand command)
    {
        var ad = await Context.Ads.FirstOrDefaultAsync(a => a.Id.Equals(command.Id) && a.UserId.Equals(userId));
        if (ad is null)
            return new BaseResult<AdViewModel>("Объявление не найдено");

        ad.StatusId = StateStatus.Deleted;
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult<AdViewModel>("Произошла ошибка при сохранении данных"),
            _ => new BaseResult<AdViewModel>(new AdViewModel(ad))
        };
    }

    /// <summary>
    /// Добавить объявление в избранное
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<BaseResult> AdAddToFavorite(
        Guid userId,
        AdAddToFavoriteCommand command)
    {
        var ad = await Context.Ads.FirstOrDefaultAsync(a => a.Id.Equals(command.AdId));
        if (ad is null)
            return new BaseResult<AdViewModel>("Объявление не найдено");

        var user = await Context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));

        var favorite = new Favorite()
        {
            AdId = command.AdId,
            UserId = userId,
        };

        Context.Favorites.Add(favorite);
        
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult<AdViewModel>("Произошла ошибка при сохранении данных"),
            _ => new BaseResult<AdViewModel>(new AdViewModel(ad))
        };
    }
    
    // todo пожаловаться 
    /// <summary>
    /// Оставить жалобу на объявление
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<BaseResult> AdToComplaint(
        Guid userId,
        AdToComplaintCommand command)
    {
        var ad = await Context.Ads.FirstOrDefaultAsync(a => a.Id.Equals(command.AdId));
        if (ad is null)
            return new BaseResult<AdViewModel>("Объявление не найдено");
    
        var user = await Context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
    
        var complaint = new Complaint()
        {
            AdId = command.AdId,
            UserId = userId,
            Description = command.Description
        };
    
        Context.Complaints.Add(complaint);
            
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult<AdViewModel>("Произошла ошибка при сохранении данных"),
            _ => new BaseResult<AdViewModel>()
        };
    }
}