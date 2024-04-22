using Microsoft.EntityFrameworkCore;
using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Responce.Result.BaseResult;
using TheArmory.Domain.Models.Responce.ViewModels.Condition;

namespace TheArmory.Repository;

public class ConditionsRepository : BaseRepository
{
    public ConditionsRepository(
        ApplicationContext context,
        ILogger<BaseRepository<Condition>> logger)
        : base(context, logger) { }

    public async Task<BaseQueryResult<ConditionListViewModel>> GetSelectList()
    {
        var conditions = await Context.Conditions
            .Select(s => new ConditionListViewModel(s))
            .ToListAsync();

         return new BaseQueryResult<ConditionListViewModel>(conditions);
    }
}