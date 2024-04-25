using TheArmory.Context;
using TheArmory.Domain.Models.Database;

namespace TheArmory.Utils.Initializer;

public class CategoryInitializer : Initializer
{
    private static readonly List<Category> Categories = new()
    {
        new Category("Охотничье оружие"),
        new Category("Экипировка"),
        new Category("Сейфы"),
        new Category("Кобуры и чехлы"),
        new Category("Аксессуары"),
        new Category("Прочее"),
    };

    /// <summary>
    /// Добавляет дефолтные категории, если их нет в БД
    /// </summary>
    /// <param name="context"></param>
    public static async Task InitializeAsync(ApplicationContext context)
    {
        if (!context.Categories.Any())
        {
            await context.Categories.AddRangeAsync(Categories);
            await context.SaveChangesAsync();
        }
    }
}