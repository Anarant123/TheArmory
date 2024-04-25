using TheArmory.Context;
using TheArmory.Domain.Models.Database;

namespace TheArmory.Utils.Initializer;

public class BarrelPositionInitializer : Initializer
{
    private static readonly List<BarrelPosition> BarrelPositions = new()
    {
        new BarrelPosition("Одноствольное"),
        new BarrelPosition("Горизонтальное"),
        new BarrelPosition("Вертикальное"),
    };

    /// <summary>
    /// Добавляет дефолтные категории, если их нет в БД
    /// </summary>
    /// <param name="context"></param>
    public static async Task InitializeAsync(ApplicationContext context)
    {
        if (!context.BarrelPositions.Any())
        {
            await context.BarrelPositions.AddRangeAsync(BarrelPositions);
            await context.SaveChangesAsync();
        }
    }
}