using System.Net.Http.Headers;
using System.Text.Json;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Web.Models;
using TheArmory.Web.Utils;

namespace TheArmory.Web.Service;

public abstract class BaseService<TEntity> : BaseService
{
    protected readonly HttpClient httpClient;

    protected readonly BaseUrlOptions baseUrlOptions;
    protected readonly ILogger<BaseService<TEntity>> logger;
    
    protected override string RootPointName => $"{typeof(TEntity).Name}s";

    protected BaseService(
        HttpClient httpClient,
        BaseUrlOptions baseUrlOptions,
        ILogger<BaseService<TEntity>> logger) 
        : base(httpClient, baseUrlOptions, logger, $"{typeof(TEntity).Name}s")
    {
        this.httpClient = httpClient;
        this.baseUrlOptions = baseUrlOptions;
        this.logger = logger;
    }

    public virtual async Task<HttpResponseMessage> GetItemsResponse(BaseQueryItemsParams? parameters = null) 
        => await base.GetItemsResponse(parameters);

    public virtual async Task<BaseQueryResult<TEntity>?> GetItemsResponseResult(
        Dictionary<string, object>? parameters = null) 
        => await base.GetItemsResponseResult<TEntity>(parameters);

    public virtual async Task<BaseQueryResult<TEntity>?> GetItemsResponseResultWithCustomPath(
        string path,
        Dictionary<string, object>? parameters = null)
        => await base.GetItemsResponseResultWithCustomPath<TEntity>(path, parameters);

    public virtual async Task<BaseResult<TEntity>?> GetByIdResponseResult(
        int id,
        Dictionary<string, string>? parameters = null)
        => await base.GetByIdResponseResult<TEntity>(id, parameters);

    public virtual async Task<BaseResult<TEntity>?> UpdateResult(
        int id,
        TEntity model,
        Dictionary<string, string>? parameters = null)
        => await base.UpdateResult<TEntity>(id, model, parameters);

    public virtual async Task<BaseResult<TEntity>?> CreateResult(
        TEntity model)
        => await base.CreateResult<TEntity>(model);

    public virtual async Task<BaseResult?> DeleteResult(
        int id)
        => await base.DeleteResult<TEntity>(id);
}

public class BaseService
{
    private readonly HttpClient httpClient;

    private readonly BaseUrlOptions baseUrlOptions;
    private readonly ILogger<BaseService> logger;

    protected virtual string RootPointName { get; set; }

    protected BaseService(
        HttpClient httpClient,
        BaseUrlOptions baseUrlOptions,
        ILogger<BaseService> logger,
        string rootPointName)
    {
        this.httpClient = httpClient;
        this.baseUrlOptions = baseUrlOptions;
        this.logger = logger;
        RootPointName = rootPointName;
    }

    public async Task<HttpResponseMessage> GetItemsResponse(
        BaseQueryItemsParams? parameters = null)
    {
        try
        {
            var uri = baseUrlOptions.GetFullApiUrl(RootPointName);
            var response = await httpClient.GetAsync(uri);
            return response;
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
            return new HttpResponseMessage();
        }
    }
    
    public async Task<BaseQueryResult<TEntity>?> GetItemsResponseResult<TEntity>(
        Dictionary<string, object>? parameters = null)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}?{parameters?.ToGetParameters()}";
            var response = await httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new BaseQueryResult<TEntity>(await response.Content.ReadAsStringAsync());
            var responseStream = await response.Content.ReadAsStreamAsync();
            var t = await response.Content.ReadAsStringAsync();
            return await JsonSerializer.DeserializeAsync<BaseQueryResult<TEntity>>(responseStream);
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
            return new BaseQueryResult<TEntity>(exception.Message);
        }
    }
    
    public async Task<BaseQueryResult<TEntity>?> GetItemsResponseResultWithCustomPath<TEntity>(
        string path,
        Dictionary<string, object>? parameters = null)
    {
        try
        {
            path = string.IsNullOrEmpty(path) ? "" : $"/{path}";
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}{path}?{parameters?.ToGetParameters()}";
            var response = await httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new BaseQueryResult<TEntity>(await response.Content.ReadAsStringAsync());
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<BaseQueryResult<TEntity>>(responseStream);
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
            return new BaseQueryResult<TEntity>(exception.Message);
        }
    }
    
    public async Task<BaseResult<TEntity>?> GetByIdResponseResult<TEntity>(
        int id,
        Dictionary<string, string>? parameters = null)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/{id}?{parameters?.ToGetParameters()}";
            var response = await httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new BaseResult<TEntity>(await response.Content.ReadAsStringAsync());
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<BaseResult<TEntity>>(responseStream);
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
            return new BaseResult<TEntity>(exception.Message);
        }
    }
    
    public async Task<BaseResult<TEntity>?> UpdateResult<TEntity>(
        int id,
        TEntity model,
        Dictionary<string, string>? parameters = null)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/{id}?{parameters?.ToGetParameters()}";
            using var content = new StringContent(JsonSerializer.Serialize(model), MediaTypeHeaderValue.Parse("application/json-patch+json"));
            var response = await httpClient.PutAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                return new BaseResult<TEntity>(await response.Content.ReadAsStringAsync());
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<BaseResult<TEntity>>(responseStream);
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
            return new BaseResult<TEntity>(exception.Message);
        }
    }
    
    public async Task<BaseResult<TEntity>?> CreateResult<TEntity>(
        TEntity model)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}";
            using var content = new StringContent(JsonSerializer.Serialize(model), MediaTypeHeaderValue.Parse("application/json-patch+json"));
            var response = await httpClient.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
                return new BaseResult<TEntity>(await response.Content.ReadAsStringAsync());
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<BaseResult<TEntity>>(responseStream);
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
            return new BaseResult<TEntity>(exception.Message);
        }
    }
    
    public async Task<BaseResult?> DeleteResult<TEntity>(
        int id)
    {
        try
        {
            var uri = $"{baseUrlOptions.GetFullApiUrl(RootPointName)}/{id}";
            var response = await httpClient.DeleteAsync(uri);
            if (!response.IsSuccessStatusCode)
                return new BaseResult(await response.Content.ReadAsStringAsync());
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<BaseResult>(responseStream);
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
            return new BaseResult(exception.Message);
        }
    }
}