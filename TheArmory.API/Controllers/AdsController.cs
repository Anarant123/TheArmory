using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Request.Commands.Ad;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Ad;
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
    /// Получение выбранного объявления
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    [Route("Selected")]
    public async Task<ActionResult<BaseResult<AdViewModel>>> GetSelected()
    {
        var userResponse = await GetUser();
        if (!userResponse.Success)
            return BadRequest(userResponse);

        var adIdResponse = GetSelectedAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);

        var result = await _adsRepository.GetAd(adIdResponse?.Item ?? Guid.Empty);

        if (result.Success) return Ok(result);
        Logger.LogError(result.Error);
        return BadRequest(result);
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
        [FromQuery]TileAdQueryItemsParams queryItemsParams)
    {
        var userResponse = await GetUser();
        if (!userResponse.Success || userResponse.Item is null)
            return BadRequest(userResponse);
        
        var result = await _adsRepository.GetMyAds(userResponse.Item.Id, queryItemsParams);
        
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Получить все объявления
    /// </summary>
    /// <param name="adId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("My/{adId:guid}")]
    public async Task<ActionResult<BaseResult<MyAdViewModel>>> GetMyAd(
        Guid adId)
    {
        var userResponse = await GetUser();
        if (!userResponse.Success || userResponse.Item is null)
            return BadRequest(userResponse);
        
        var result = await _adsRepository.GetMyAd(userResponse.Item.Id, adId);
        
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
    /// Получить все данные необходимые для публикации объявления
    /// </summary>
    /// <param name="adId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("PublishInformation")]
    public async Task<ActionResult<BaseQueryResult<AdPublishInfoViewModel>>> GetPublishInformation()
    {
        var result = await _adsRepository.GetPublishInformation();
        
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
        
        var adIdResponse = GetSelectedAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);

        var result = await _adsRepository.AdAddToFavorite(
            userResponse.Item.Id,
            adIdResponse.Item);

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
    [Route("Complaint")]
    public async Task<ActionResult<BaseResult>> AdToComplaint(
        [FromBody]AdToComplaintCommand command)
    {
        var userResponse = await GetUser();
        if (userResponse.Item is null)
            return BadRequest(userResponse);
        
        var adIdResponse = GetSelectedAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);
        command.Id = adIdResponse.Item;

        var result = await _adsRepository.AdToComplaint(
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
    
    [HttpPost]
    [Route("Select")]
    public async Task<ActionResult<BaseResult<AdViewModel>>> Select(
        [FromBody]AdSelectCommand command)
    {
        var result = await _adsRepository.GetAd(command.Id);

        if (result.Success)
        {
            HttpContext.Session.SetString("SelectedAd", command.Id.ToString());
            return Ok(result);
        }

        HttpContext.Session.Remove("SelectedAd");
        Logger.LogError(result.Error);
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