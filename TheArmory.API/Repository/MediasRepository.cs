using Microsoft.EntityFrameworkCore;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Responce.Result.BaseResult;

namespace TheArmory.Repository;

public class MediasRepository : BaseRepository<Media>
{
    private string FilesPath { get; init; }

    public MediasRepository(
        ApplicationContext context,
        ILogger<BaseRepository<Media>> logger,
        IConfiguration configuration)
        : base(context, logger)
    {
        FilesPath = Path.Combine(configuration.GetSection("FilesPath").Value ?? "Files");
    }

    /// <summary>
    /// Сохраняет фотографии объявления в файловой системе
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="adId"></param>
    /// <param name="files"></param>
    /// <returns></returns>
    public async Task<BaseResult<List<Media>>> SaveAdFile(
        Guid userId,
        Guid adId,
        List<IFormFile> files)
    {
        var userFilePath = Path.Combine(FilesPath, userId.ToString());
        EnsureDirectoryExists(userFilePath);

        var adsFilePath = Path.Combine(userFilePath, "Ads");
        EnsureDirectoryExists(adsFilePath);

        var adFilePath = Path.Combine(adsFilePath, adId.ToString());
        EnsureDirectoryExists(adFilePath);

        var medias = new List<Media>();
        foreach (var file in files)
        {
            if (file.Length <= 0) continue;
            var filePath = Path.Combine(adFilePath, file.FileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var media = new Media
            {
                Name = file.FileName,
                AdId = adId,
            };
            medias.Add(media);
        }

        return new BaseResult<List<Media>>(medias);
    }

    /// <summary>
    /// Удаляет фотографию объявления
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="adId"></param>
    /// <param name="mediaId"></param>
    /// <returns></returns>
    public async Task<BaseResult> DeleteAdFile(
        Guid userId,
        Guid adId,
        Guid mediaId)
    {
        var media = await Context.Medias.FirstOrDefaultAsync(m => m.Id.Equals(mediaId));

        if (media is null)
            return new BaseResult("Такой фотографии не существует");

        var userFilePath = Path.Combine(FilesPath, userId.ToString());
        var adsFilePath = Path.Combine(userFilePath, "Ads");
        var adFilePath = Path.Combine(adsFilePath, adId.ToString());
        var mediaFilePath = Path.Combine(adFilePath, media.Name);

        if (!File.Exists(mediaFilePath)) return new BaseResult("Файл не найден");

        File.Delete(mediaFilePath);
        Context.Medias.Remove(media);
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult("Произошла ошибка при сохранении данных"),
            _ => new BaseResult()
        };
    }


    /// <summary>
    /// Добавляет фотографию к объявлению
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<BaseResult<Media>> AddAdFile(
        Guid userId,
        AdAddMediaCommand command)
    {
        var userFilePath = Path.Combine(FilesPath, userId.ToString());
        var adsFilePath = Path.Combine(userFilePath, "Ads");
        var adFilePath = Path.Combine(adsFilePath, command.Id.ToString());
        var filePath = Path.Combine(adFilePath, command.Photo.FileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await command.Photo.CopyToAsync(stream);

        var media = new Media
        {
            Name = command.Photo.FileName,
            AdId = command.Id,
        };

        return new BaseResult<Media>(media);
    }

    /// <summary>
    /// Добавляет фотографию к объявлению
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    public async Task<BaseResult<string>> ChangeProfilePhoto(
        Guid userId,
        IFormFile photo)
    {
        var userFilePath = Path.Combine(FilesPath, userId.ToString());
        var profileInfoFilePath = Path.Combine(userFilePath, "ProfileInfo");
        EnsureDirectoryExists(profileInfoFilePath);
        DeleteAllFilesInProfileInfo(profileInfoFilePath);

        var fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
        var filePath = Path.Combine(profileInfoFilePath, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await photo.CopyToAsync(stream);

        return new BaseResult<string>()
        {
            Item = fileName
        };
    }


    public async Task<List<Media>> GetMediaByAdId(Guid adId)
    {
        var medias = await Context.Medias
            .Where(m => m.AdId.Equals(adId))
            .ToListAsync();

        return medias;
    }


    private static void EnsureDirectoryExists(string directoryPath)
    {
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
    }


    private static void DeleteAllFilesInProfileInfo(string path)
    {
        try
        {
            var files = Directory.GetFiles(path);

            foreach (var file in files) File.Delete(file);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении файлов из директории: {ex.Message}");
        }
    }
}