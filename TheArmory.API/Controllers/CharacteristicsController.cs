using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Request.Commands.Characteristic;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Repository;

namespace TheArmory.Controllers;

[Route("Characteristics")]
[ApiController]
public class CharacteristicsController : BaseController
{
    private readonly ILogger<ConditionsController> Logger;
    private readonly CharacteristicsRepository _characteristicsRepository;
    
    public CharacteristicsController(
        ILogger<ConditionsController> logger,
        UsersRepository usersRepository,
        CharacteristicsRepository characteristicsRepository) 
        : base(logger, usersRepository)
    {
        Logger = logger;
        _characteristicsRepository = characteristicsRepository;
    }
    
    /// <summary>
    /// Создать характеристику
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    public async Task<ActionResult<BaseResult>> CreateCharacteristic(
        [FromBody]CharacteristicCreateCommand command)
    {
        var userResponse = await GetUser();
        if (!userResponse.Success)
            return userResponse;
        
        var adIdResponse = GetSelectedMyAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);

        var result = await _characteristicsRepository.Create(
            adIdResponse.Item,
            command);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Удалить характеристику
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("")]
    public async Task<ActionResult<BaseResult>> DeleteCharacteristic(
        [FromQuery]CharacteristicCommand command)
    {
        var userResponse = await GetUser();
        if (!userResponse.Success)
            return userResponse;
        
        var adIdResponse = GetSelectedMyAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);

        var result = await _characteristicsRepository.Delete(
            adIdResponse.Item,
            command);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
}