using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Utils.Initializer;

public class RoleInitializer : Initializer
{
    private static readonly List<Role> Roles = new()
    {
        new Role(UserRole.SuperAdmin, "Супер админ"),
        new Role(UserRole.Admin, "Админ"),
        new Role(UserRole.Client, "Клиент")
    };

    /// <summary>
    /// Добавляет дефолтные роли, если их нет в БД
    /// </summary>
    /// <param name="context"></param>
    public static async Task InitializeAsync(ApplicationContext context)
    {
        if (!context.Roles.Any())
        {
            await context.Roles.AddRangeAsync(Roles);
            await context.SaveChangesAsync();
        }
    }
}