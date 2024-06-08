using TheArmory.Context;
using TheArmory.Domain.Models.Database;

namespace TheArmory.Repository;

public class BaseRepository
{
    protected readonly ApplicationContext Context;
    protected readonly ILogger<BaseRepository> Logger;

    public BaseRepository(
        ApplicationContext context,
        ILogger<BaseRepository> logger)
    {
        Context = context;
        Logger = logger;
    }
}

public class BaseRepository<TEntity> : BaseRepository where TEntity : DbEntity
{
    public BaseRepository(ApplicationContext context, ILogger<BaseRepository> logger) : base(context, logger)
    {
    }
}