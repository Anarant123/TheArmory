using Microsoft.EntityFrameworkCore;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Condition;
using TheArmory.Domain.Models.Responce.ViewModels.Region;

namespace TheArmory.Repository;

public class RegionsRepository : BaseRepository<Region>
{
    public RegionsRepository(
        ApplicationContext context,
        ILogger<BaseRepository<Region>> logger)
        : base(context, logger) { }

    public async Task<BaseQueryResult<RegionListViewModel>> GetSelectList()
    {
        var conditions = await Context.Regions
            .Select(s => new RegionListViewModel(s))
            .ToListAsync();
        
        return new BaseQueryResult<RegionListViewModel>(conditions);
    }

}