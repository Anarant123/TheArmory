using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Utils.Initializer;

public class ConditionInitializer : Initializer
{
    private static readonly List<Condition> Conditions = new()
    {
        new Condition(WeaponCondition.New, "Новая"),
        new Condition(WeaponCondition.Perfect, "Идеальное"),
        new Condition(WeaponCondition.Fine, "Хорошее"),
        new Condition(WeaponCondition.NeedsRepair, "Требует ремонта"),
    };

    /// <summary>
    /// Добавляет дефолтные роли, если их нет в БД
    /// </summary>
    /// <param name="context"></param>
    public static async Task InitializeAsync(ApplicationContext context)
    {
        if (!context.Conditions.Any())
        {
            await context.Conditions.AddRangeAsync(Conditions);
            await context.SaveChangesAsync();
        }
    }
}