using Microsoft.EntityFrameworkCore;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.Characteristic;
using TheArmory.Domain.Models.Responce.Result.BaseResult;

namespace TheArmory.Repository;

public class CharacteristicsRepository : BaseRepository<Characteristic>
{
    public CharacteristicsRepository(
        ApplicationContext context,
        ILogger<BaseRepository<Characteristic>> logger)
        : base(context, logger)
    {
    }

    public async Task<BaseResult> Create(
        Guid adId,
        CharacteristicCreateCommand command)
    {
        var characteristic = new Characteristic()
        {
            Name = command.Name,
            Description = command.Description,
            AdId = adId
        };

        Context.Characteristics.Add(characteristic);
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult("Произошла ошибка при сохранении данных"),
            _ => new BaseResult()
        };
    }
    
    public async Task<BaseResult> Delete(
        Guid adId,
        CharacteristicCommand command)
    {
        var contact = await Context.Characteristics.FirstOrDefaultAsync(c => c.Id.Equals(command.Id));

        if (contact is null)
            return new BaseResult("Контакт не найден");

        Context.Characteristics.Remove(contact);
        
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult(ErrorsMessage.ErrorSavingChanges),
            _ => new BaseResult()
        };
    }
}