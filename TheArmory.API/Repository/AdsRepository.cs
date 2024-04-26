using Microsoft.EntityFrameworkCore;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;

namespace TheArmory.Repository;

public class AdsRepository : BaseRepository
{
    private readonly MediasRepository _mediasRepository;
    public AdsRepository(
        ApplicationContext context,
        ILogger<BaseRepository<Ad>> logger,
        MediasRepository mediasRepository)
        : base(context, logger)
    {
        _mediasRepository = mediasRepository;
    }
    
    /// <summary>
    /// Возвращает объявление
    /// </summary>
    /// <param name="adId"></param>
    /// <returns></returns>
    public async Task<BaseResult<AdViewModel>> GetAd(
        Guid adId)
    {
        var ad = await Context.Ads
            .Include(a => a.Medias)
            .Include(a => a.Condition)
            .Include(a => a.Region)
            .Include(a => a.User)
            .ThenInclude(u => u.Contacts)
            .Include(a => a.User)
            .ThenInclude(u => u.Ads)
            .Include(a => a.Location)
            .FirstOrDefaultAsync(a => a.Id.Equals(adId));

        if (ad is null)
            return new BaseResult<AdViewModel>("Объявление не найдено");

        ad.CountOfViews += 1;
        if (ad.LastVisitDate.Date != DateTime.Now.Date)
        {
            ad.LastVisitDate = DateTime.Now;
            ad.CountOfViewsToday = 0;
        }
        ad.CountOfViewsToday += 1;

        await Context.SaveChangesAsync();

        if (ad.CharacteristicId is null) return new BaseResult<AdViewModel>(new AdViewModel(ad));
        
        var characteristic = await Context.Characteristics
            .AsNoTracking()
            .Include(c => c.BarrelPosition)
            .Include(c => c.WeaponType)
            .Include(c => c.Caliber)
            .FirstOrDefaultAsync(c => c.Id.Equals(ad.CharacteristicId));
            
        return new BaseResult<AdViewModel>(new AdViewModel(ad, characteristic!));

    }

    /// <summary>
    /// Возвращает объявление пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="adId"></param>
    /// <returns></returns>
    public async Task<BaseResult<MyAdViewModel>> GetMyAd(
        Guid userId,
        Guid adId)
    {
        var ad = await Context.Ads
            .Include(a => a.Medias)
            .Include(a => a.Condition)
            .Include(a => a.Region)
            .FirstOrDefaultAsync(a => a.Id.Equals(adId));

        if (ad is null)
            return new BaseResult<MyAdViewModel>("Объявление не найдено");
        
        return new BaseResult<MyAdViewModel>(new MyAdViewModel(ad));
    }
    
    /// <summary>
    /// Возвращает все объявления пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<BaseQueryResult<TileAdViewModel>> GetMyAds(
        Guid userId,
        TileAdQueryItemsParams queryItemsParams)
    {
        var ads = await Context.Ads
            .Include(a => a.Medias)
            .Include(a => a.Condition)
            .Include(a => a.Region)
            .Where(a => a.UserId.Equals(userId) 
                        && a.Status.Id.Equals(queryItemsParams.StatusId))
            .Select(s => new TileAdViewModel(s))
            .ToListAsync();

        if (ads.Count == 0)
            return new BaseQueryResult<TileAdViewModel>("Объявлений не найдено");

        return new BaseQueryResult<TileAdViewModel>(ads);
    }

    /// <summary>
    /// Возвращает все данные необходимые для публикации объявления
    /// </summary>
    /// <returns></returns>
    public async Task<BaseResult<AdPublishInfoViewModel>> GetPublishInformation()
    {
        var conditions = await Context.Conditions.ToListAsync();
        var categories = await Context.Categories.ToListAsync();
        var calibers = await Context.Calibers.ToListAsync();
        var weaponTypes = await Context.WeaponTypes.ToListAsync();
        var barrelPositions = await Context.BarrelPositions.ToListAsync();

        var result = new AdPublishInfoViewModel
        {
            Conditions = conditions,
            Categories = categories,
            Calibers = calibers,
            WeaponTypes = weaponTypes,
            BarrelPositions = barrelPositions
        };

        return new BaseResult<AdPublishInfoViewModel>(result) { };
    }

    
    /// <summary>
    /// Возвращает все объявления 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<BaseQueryResult<TileAdViewModel>> GetAds(
        Guid? userId)
    {
        var ads = await Context.Ads
            .Include(a => a.Medias)
            .Where(a => userId != null 
                ? !a.UserId.Equals(userId) 
                : true 
                  && a.StatusId.Equals(StateStatus.Actively))
            .Select(s => new TileAdViewModel(s))
            .ToListAsync();


        if (ads.Count == 0)
            return new BaseQueryResult<TileAdViewModel>("Объявлений не найдено");

        return new BaseQueryResult<TileAdViewModel>(ads);
    }
    
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
        if (command.Photos.Count > 5)
            return new BaseResult<AdViewModel>("Превышено максимально допустимое кол во фотографий");
        
        var newAd = new Ad()
        {
            Name = command.Name,
            Price = command.Price,
            Description = command.Description ?? "",
            YouTubeLink = command.YouTubeLink,
            ConditionId = command.ConditionId,
            UserId = userId,
        };

        var address = command.Address ?? string.Empty;

        var region = await Context.Regions.FirstOrDefaultAsync(r => address.ToLower().Contains(r.Name.ToLower()));
        if (region is not null)
            newAd.RegionId = region.Id;

        var category = await Context.Categories.FirstOrDefaultAsync(c => c.Id.Equals(command.CategoryId));
        if (category is not null)
        {
            newAd.CategoryId = category.Id;
            if (category.Name.Equals("Охотничье оружие") 
                && command.WeaponTypeId is not null
                && command.CaliberId is not null
                && command.BarrelPositionId is not null
                && command.YearOfProduction is not null)
            {
                var characteristic = new Characteristic()
                {
                    WeaponTypeId = (Guid)command.WeaponTypeId,
                    CaliberId = (Guid)command.CaliberId,
                    BarrelPositionId = (Guid)command.BarrelPositionId,
                    YearOfProduction = (int)command.YearOfProduction
                };
                newAd.Characteristic = characteristic;
            }
            else if (category.Name.Equals("Охотничье оружие"))
            {
                return new BaseResult<AdViewModel>("Характеристики необходимо заполнить до конца");
            }
        }
        
        if (!string.IsNullOrEmpty(command.Latitude) && !string.IsNullOrEmpty(command.Longitude))
        {
            newAd.Location = new Location()
            {
                Address = address,
                Latitude = Convert.ToDouble(command.Latitude.Replace('.', ',')),
                Longitude = Convert.ToDouble(command.Longitude.Replace('.', ',')),
            };
        }
        
        var saveResult = await _mediasRepository.SaveAdFile(userId, newAd.Id, command.Photos);
        if (!saveResult.Success) return new BaseResult<AdViewModel>("Произошла ошибка при сохранении данных");

        
        newAd.Medias = saveResult.Item;

        Context.Ads.Add(newAd);
        if (await Context.SaveChangesAsync() == 0)
            return new BaseResult<AdViewModel>("Произошла ошибка при сохранении данных");

        var ad = await Context.Ads
            .Include(a => a.Condition)
            .Include(a => a.Medias)
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id.Equals(newAd.Id));

        return new BaseResult<AdViewModel>(new AdViewModel(newAd));
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
            ad.YouTubeLink = command.YouToubeLink;
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
    /// <param name="adId"></param>
    /// <returns></returns>
    public async Task<BaseResult> AdAddToFavorite(
        Guid userId,
        Guid adId)
    {
        var ad = await Context.Ads.FirstOrDefaultAsync(a => a.Id.Equals(adId));
        if (ad is null)
            return new BaseResult<AdViewModel>("Объявление не найдено");

        var user = await Context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));

        var favorite = new Favorite()
        {
            AdId = adId,
            UserId = userId,
        };

        Context.Favorites.Add(favorite);
        
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult<AdViewModel>("Произошла ошибка при сохранении данных"),
            _ => new BaseResult<AdViewModel>(new AdViewModel(ad))
        };
    }
    
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
        var ad = await Context.Ads.FirstOrDefaultAsync(a => a.Id.Equals(command.Id));
        if (ad is null)
            return new BaseResult<AdViewModel>("Объявление не найдено");
    
        var user = await Context.Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
    
        var complaint = new Complaint()
        {
            AdId = command.Id,
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
    
    
    /// <summary>
    /// Удалить фото
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<BaseResult> DeleteMedia(
        Guid userId,
        AdDeleteMediaCommand command)
    {
        var result = await _mediasRepository.DeleteAdFile(userId, command.Id, command.MediaId);
        if (!result.Success)
            return new BaseResult(result.Error);

        return new BaseResult();
    }
    
    /// <summary>
    /// Добавить фото
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<BaseResult> AddMedia(
        Guid userId,
        AdAddMediaCommand command)
    {
        var result = await _mediasRepository.AddAdFile(userId, command);
        if (!result.Success)
            return new BaseResult(result.Error);

        return new BaseResult();
    }
}