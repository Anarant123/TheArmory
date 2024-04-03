using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Utils.Initializer;

public class StatusInitializer : Initializer
{
    private static readonly List<Status> Statuses = new()
    {
        new Status(StateStatus.Actively, "Активный"),
        new Status(StateStatus.Inactive, "Неактивный"),
        new Status(StateStatus.Deleted, "Забанен"),
        new Status(StateStatus.Banned, "Удален")
    };

    /// <summary>
    /// Добавляет дефолтные роли, если их нет в БД
    /// </summary>
    /// <param name="context"></param>
    public static async Task InitializeAsync(ApplicationContext context)
    {
        if (!context.Statuses.Any())
        {
            await context.Statuses.AddRangeAsync(Statuses);
            await context.SaveChangesAsync();
        }
    }
}