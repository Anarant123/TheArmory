using TheArmory.Context;
using TheArmory.Domain.Models.Database;

namespace TheArmory.Utils.Initializer;

public class WeaponTypeInitializer : Initializer
{
    private static readonly List<WeaponType> WeaponTypes = new()
    {
        new WeaponType("Гладкоствольное"),
        new WeaponType("Нарезное"),
        new WeaponType("Комбинированное"),
        new WeaponType("Пневматическое"),
    };

    /// <summary>
    /// Добавляет дефолтные категории, если их нет в БД
    /// </summary>
    /// <param name="context"></param>
    public static async Task InitializeAsync(ApplicationContext context)
    {
        if (!context.WeaponTypes.Any())
        {
            await context.WeaponTypes.AddRangeAsync(WeaponTypes);
            await context.SaveChangesAsync();
        }
    }
}