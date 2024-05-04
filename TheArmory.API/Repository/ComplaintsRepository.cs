using Microsoft.EntityFrameworkCore;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Request.Queries;
using TheArmory.Domain.Models.Responce.Result.BaseResult;

namespace TheArmory.Repository;

public class ComplaintsRepository : BaseRepository<Complaint>
{
    public ComplaintsRepository(
        ApplicationContext context,
        ILogger<BaseRepository<Complaint>> logger)
        : base(context, logger)
    {
    }

    public async Task<BaseQueryResult<Complaint>> Get(
        Guid adId,
        BaseQueryItemsParams queryItemsParams)
    {
        var complaints = await Context.Complaints
            .Where(c => c.AdId.Equals(adId))
            .ToListAsync();

        return new BaseQueryResult<Complaint>(complaints);
    }
}