using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Request.Commands.Contact;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Repository;

namespace TheArmory.Controllers;

[Route("Contacts")]
[ApiController]
public class ContactsController : BaseController
{
    protected readonly ILogger<ConditionsController> Logger;
    private readonly ContactsRepository _contactsRepository;
    
    public ContactsController(
        ILogger<ConditionsController> logger,
        UsersRepository usersRepository,
        ContactsRepository contactsRepository) 
        : base(logger, usersRepository)
    {
        Logger = logger;
        _contactsRepository = contactsRepository;
    }
    
    /// <summary>
    /// Разместить объявление
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    public async Task<ActionResult<BaseResult<Contact>>> CreateContact(
        [FromBody]ContactCreateCommand command)
    {
        var userResponse = await GetUser();

        var result = await _contactsRepository.Create(
            userResponse.Item.Id,
            command);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    
    /// <summary>
    /// Разместить объявление
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("")]
    public async Task<ActionResult<BaseResult>> DeleteContact(
        [FromQuery]ContactCommand command)
    {
        var userResponse = await GetUser();
        if (!userResponse.Success)
            return userResponse;

        var result = await _contactsRepository.Delete(
            userResponse.Item.Id,
            command);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
}