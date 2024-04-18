using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
using TheArmory.Domain.Models.Responce.ViewModels.User;
using TheArmory.Repository;

namespace TheArmory.Controllers;


[Route("Ads")]
[ApiController]
public class AdsController : BaseController
{
    protected readonly ILogger<ConditionsController> Logger;
    private readonly AdsRepository _adsRepository;
    
    public AdsController(
        ILogger<ConditionsController> logger,
        UsersRepository usersRepository,
        AdsRepository adsRepository) 
        : base(logger, usersRepository)
    {
        Logger = logger;
        _adsRepository = adsRepository;
    }

    /// <summary>
    /// Получить все объявления
    /// </summary>
    /// <param name="queryItemsParams"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<BaseQueryResult<TileAdViewModel>>> GetAds(
    [FromQuery]BaseQueryItemsParams queryItemsParams)
    {
        var userResponse = await GetUser();
        
        var result = await _adsRepository.GetAds(userResponse.Item?.Id);
        
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Получить все объявления
    /// </summary>
    /// <param name="queryItemsParams"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("My")]
    public async Task<ActionResult<BaseQueryResult<TileAdViewModel>>> GetMyAds(
        [FromQuery]BaseQueryItemsParams queryItemsParams)
    {
        var userResponse = await GetUser();
        if (!userResponse.Success || userResponse.Item is null)
            return BadRequest(userResponse);
        
        var result = await _adsRepository.GetMyAds(userResponse.Item.Id);
        
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Получить объявление
    /// </summary>
    /// <param name="adId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{adId:guid}")]
    public async Task<ActionResult<BaseQueryResult<TileAdViewModel>>> GetAd(
        Guid adId)
    {
        var result = await _adsRepository.GetAd(adId);
        
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Разместить объявление
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("")]
    public async Task<ActionResult<BaseResult<AdViewModel>>> PostAd(
        [FromForm]AdCreateCommand command)
    {
        var userResponse = await GetUser();

        var result = await _adsRepository.Create(
            userResponse.Item.Id,
            command);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Добавить в избранное
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("ToFavorite")]
    public async Task<ActionResult<BaseResult>> AddToFavorite(
        [FromBody]AdCommand command)
    {
        var userResponse = await GetUser();
        if (userResponse.Item is null)
            return BadRequest(userResponse);

        var result = await _adsRepository.AdAddToFavorite(
            userResponse.Item.Id,
            command);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Удалить фото объявления
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Media")]
    public async Task<ActionResult<BaseResult>> AddMedia(
        [FromForm]AdAddMediaCommand command)
    {
        var userResponse = await GetUser();
        if (userResponse.Item is null)
            return BadRequest(userResponse);

        var result = await _adsRepository.AddMedia(
            userResponse.Item.Id,
            command);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Удалить фото объявления
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("Media")]
    public async Task<ActionResult<BaseResult>> DeleteMedia(
        [FromForm]AdDeleteMediaCommand command)
    {
        var userResponse = await GetUser();
        if (userResponse.Item is null)
            return BadRequest(userResponse);

        var result = await _adsRepository.DeleteMedia(
            userResponse.Item.Id,
            command);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    
}