using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheArmory.Domain.Models.Enums;
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
    private readonly ILogger<ConditionsController> Logger;
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
    /// Выбранное объявление
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("Selected")]
    public async Task<ActionResult<BaseResult<AdViewModel>>> GetSelected()
    {
        var userResponse = await GetUser();

        var userId = userResponse.Item?.Id;

        var adIdResponse = GetSelectedAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);

        var result = await _adsRepository.GetAd(userId, adIdResponse?.Item ?? Guid.Empty);

        if (result.Success) return Ok(result);
        Logger.LogError(result.Error);
        return BadRequest(result);
    }
    
    /// <summary>
    /// Свое выбранное объявление
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
    [HttpGet]
    [Route("SelectedMy")]
    public async Task<ActionResult<BaseResult<MyAdViewModel>>> GetSelectedMy()
    {
        var userResponse = await GetUser();

        var userId = userResponse.Item?.Id;

        var adIdResponse = GetSelectedMyAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);

        var result = await _adsRepository.GetMyAd((Guid)userId!, adIdResponse?.Item ?? Guid.Empty);

        if (result.Success) return Ok(result);
        Logger.LogError(result.Error);
        return BadRequest(result);
    }

    /// <summary>
    /// Объявления
    /// </summary>
    /// <param name="queryItemsParams"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<BaseQueryResult<TileAdViewModel>>> GetAds(
    [FromQuery]TileAdQueryItemsParams queryItemsParams)
    {
        var userResponse = await GetUser();
        
        var result = await _adsRepository.GetAds(userResponse.Item?.Id, queryItemsParams);
        
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Забаненные объявления
    /// </summary>
    /// <param name="queryItemsParams"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("Banned")]
    public async Task<ActionResult<BaseQueryResult<TileAdViewModel>>> GetBannedAds(
        [FromQuery]BaseQueryItemsParams queryItemsParams)
    {
        var userResponse = await GetUser();
        
        var result = await _adsRepository.GetBunnedAds(queryItemsParams);
        
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Свои объявления
    /// </summary>
    /// <param name="queryItemsParams"></param>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
    [HttpGet]
    [Route("My")]
    public async Task<ActionResult<BaseQueryResult<TileAdViewModel>>> GetMyAds(
        [FromQuery]MyTileAdQueryItemsParams queryItemsParams)
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
    /// Избранные объявления
    /// </summary>
    /// <param name="queryItemsParams"></param>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
    [HttpGet]
    [Route("Favorites")]
    public async Task<ActionResult<BaseQueryResult<TileAdViewModel>>> GetFavoritesAds(
        [FromQuery]BaseQueryItemsParams queryItemsParams)
    {
        var userResponse = await GetUser();
        if (!userResponse.Success || userResponse.Item is null)
            return BadRequest(userResponse);
        
        var result = await _adsRepository.GetFavoritesAds(userResponse.Item.Id, queryItemsParams);
        
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Объявления с жалобами
    /// </summary>
    /// <param name="queryItemsParams"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("Complaints")]
    public async Task<ActionResult<BaseQueryResult<TileAdComplaintViewModel>>> GetComplaintsAds(
        [FromQuery]BaseQueryItemsParams queryItemsParams)
    {
        var userResponse = await GetUser();
        if (!userResponse.Success || userResponse.Item is null)
            return BadRequest(userResponse);
        
        var result = await _adsRepository.GetComplaintsAds(queryItemsParams);
        
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Данные необходимые для публикации объявления
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
    [HttpGet]
    [Route("PublishInformation")]
    public async Task<ActionResult<BaseResult<AdPublishInfoViewModel>>> GetPublishInformation()
    {
        var result = await _adsRepository.GetPublishInformation();
        
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }

    /// <summary>
    /// Данные необходимые для фильтрации объявлений
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("FilterViewModel")]
    public async Task<ActionResult<BaseResult<AdFilterViewModel>>> GetFilterViewModel()
    {
        var result = await _adsRepository.GetFilterViewModel();
        
        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Опубликовать объявление
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
    [HttpPost]
    [Route("")]
    public async Task<ActionResult<BaseResult<MyAdViewModel>>> PostAd(
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
    /// <returns></returns>
    [Authorize(Roles = "Client")]
    [HttpPost]
    [Route("ToFavorite")]
    public async Task<ActionResult<BaseResult>> AddToFavorite()
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
    /// Подать жалобу
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
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
    /// Забанить
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [Route("Ban")]
    public async Task<ActionResult<BaseResult>> AdBan()
    {
        var userResponse = await GetUser();
        if (userResponse.Item is null)
            return BadRequest(userResponse);
        
        var adIdResponse = GetSelectedAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);

        var result = await _adsRepository.AdBan(adIdResponse.Item);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Удалить фото объявления
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
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
    /// Выбор объявления и сохранение его в cookie
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Select")]
    public async Task<ActionResult<BaseResult<AdViewModel>>> Select(
        [FromBody]AdSelectCommand command)
    {
        var userResponse = await GetUser();

        var userId = userResponse.Item?.Id;
        
        var result = await _adsRepository.GetAd(userId, command.Id);

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
    /// Выбор своего объявления и сохранение его в cookie
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
    [HttpPost]
    [Route("SelectMy")]
    public async Task<ActionResult<BaseResult<AdViewModel>>> SelectMy(
        [FromBody]AdSelectCommand command)
    {
        var userResponse = await GetUser();
        if (userResponse.Item is null)
            return BadRequest(userResponse);
        
        var result = await _adsRepository.GetMyAd(userResponse.Item.Id, command.Id);

        if (result.Success)
        {
            HttpContext.Session.SetString("SelectedMyAd", command.Id.ToString());
            return Ok(result);
        }

        HttpContext.Session.Remove("SelectedMyAd");
        Logger.LogError(result.Error);
        return BadRequest(result);
    }
    
    /// <summary>
    /// Редактировать
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
    [HttpPut]
    [Route("")]
    public async Task<ActionResult<BaseResult>> UpdateAd(
        [FromBody]AdUpdateCommand command)
    {
        var userResponse = await GetUser();
        if (userResponse.Item is null)
            return BadRequest(userResponse);
        
        var adIdResponse = GetSelectedMyAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);
        command.Id = adIdResponse.Item;

        var result = await _adsRepository.Update(
            userResponse.Item.Id,
            command);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Снять с публикации
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
    [HttpPut]
    [Route("Deactivate")]
    public async Task<ActionResult<BaseResult>> DeactivateAd()
    {
        var userResponse = await GetUser();
        if (userResponse.Item is null)
            return BadRequest(userResponse);
        
        var adIdResponse = GetSelectedMyAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);

        var result = await _adsRepository.ChangeStateStatus(
            userResponse.Item.Id,
            adIdResponse.Item,
            StateStatus.Inactive);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Активировать объявление
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
    [HttpPut]
    [Route("Activate")]
    public async Task<ActionResult<BaseResult>> ActivateAd()
    {
        var userResponse = await GetUser();
        if (userResponse.Item is null)
            return BadRequest(userResponse);
        
        var adIdResponse = GetSelectedMyAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);

        var result = await _adsRepository.ChangeStateStatus(
            userResponse.Item.Id,
            adIdResponse.Item,
            StateStatus.Actively);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Оправдать объявление
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("Justify")]
    public async Task<ActionResult<BaseResult>> JustifyAd()
    {
        var userResponse = await GetUser();
        if (userResponse.Item is null)
            return BadRequest(userResponse);
        
        var adIdResponse = GetSelectedAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);

        var result = await _adsRepository.Justify(
            adIdResponse.Item);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Удалить фото объявления
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
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
    
    /// <summary>
    /// Удалить объявление из избранного
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
    [HttpDelete]
    [Route("Favorite")]
    public async Task<ActionResult<BaseResult>> DeleteFromFavorite()
    {
        var userResponse = await GetUser();
        if (userResponse.Item is null)
            return BadRequest(userResponse);
        
        var adIdResponse = GetSelectedAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);


        var result = await _adsRepository.DeleteFromFavorite(
            userResponse.Item.Id,
            adIdResponse.Item);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
    /// <summary>
    /// Удалить объявление
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Client")]
    [HttpDelete]
    [Route("")]
    public async Task<ActionResult<BaseResult>> DeleteAd()
    {
        var userResponse = await GetUser();
        if (userResponse.Item is null)
            return BadRequest(userResponse);
        
        var adIdResponse = GetSelectedMyAdId();
        if (!adIdResponse.Success)
            return BadRequest(adIdResponse);


        var result = await _adsRepository.ChangeStateStatus(
            userResponse.Item.Id,
            adIdResponse.Item,
            StateStatus.Deleted);

        if (result.Success)
            return Ok(result);

        return BadRequest(result);
    }
    
}