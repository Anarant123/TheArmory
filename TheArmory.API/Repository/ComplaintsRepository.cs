using Microsoft.EntityFrameworkCore;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Complaint;

namespace TheArmory.Repository;

public class ComplaintsRepository : BaseRepository<Complaint>
{
    public ComplaintsRepository(
        ApplicationContext context,
        ILogger<BaseRepository<Complaint>> logger)
        : base(context, logger)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="adId"></param>
    /// <param name="queryItemsParams"></param>
    /// <returns></returns>
    public async Task<BaseQueryResult<ComplaintViewModel>> Get(
        Guid adId,
        BaseQueryItemsParams queryItemsParams)
    {
        var complaints = await Context.Complaints
            .Include(c => c.User)
            .Where(c => c.AdId.Equals(adId))
            .Select(s => new ComplaintViewModel(s))
            .ToListAsync();

        return new BaseQueryResult<ComplaintViewModel>(complaints);
    }
}