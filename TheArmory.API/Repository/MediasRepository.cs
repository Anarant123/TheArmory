using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
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

        // перенести потом в другое место
        // var profileInfoFilePath = Path.Combine(userFilePath, "ProfileInfo");
        // EnsureDirectoryExists(userFilePath);

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
    
    
}