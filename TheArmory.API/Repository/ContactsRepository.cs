using Microsoft.EntityFrameworkCore;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Message.Errors;
using TheArmory.Domain.Models.Request.Commands.Contact;
using TheArmory.Domain.Models.Responce.Result.BaseResult;

namespace TheArmory.Repository;

public class ContactsRepository : BaseRepository
{
    public ContactsRepository(
        ApplicationContext context,
        ILogger<BaseRepository<Contact>> logger)
        : base(context, logger)
    {
    }
    
    public async Task<BaseResult<Contact>> Create(
        Guid userId,
        ContactCreateCommand command)
    {
        var user = await Context.Users
            .Include(u => u.Region)
            .Include(u => u.Status)
            .FirstOrDefaultAsync(u => u.Id.Equals(userId));
        
        if (user is null)
            return new BaseResult<Contact>(ErrorsMessage.UserNotFound);

        var contact = new Contact()
        {
            Name = command.Name,
            Description = command.Description,
            UserId = user.Id
        };

        Context.Contacts.Add(contact);
        
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult<Contact>(ErrorsMessage.ErrorSavingChanges),
            _ => new BaseResult<Contact>(contact)
        };
    }
    
    public async Task<BaseResult> Delete(
        Guid userId,
        ContactCommand command)
    {
        var contact = await Context.Contacts.FirstOrDefaultAsync(c => c.Id.Equals(command.Id));

        if (contact is null)
            return new BaseResult("Контакт не найден");

        Context.Contacts.Remove(contact);
        
        return await Context.SaveChangesAsync() switch
        {
            0 => new BaseResult<Contact>(ErrorsMessage.ErrorSavingChanges),
            _ => new BaseResult<Contact>(contact)
        };
    }
}