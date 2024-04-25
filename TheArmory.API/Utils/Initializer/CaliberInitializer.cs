using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Utils.Initializer;

public class CaliberInitializer : Initializer
{
    private static readonly List<Caliber> Calibers = new()
    {
        new Caliber(".22 LR (5,6 мм)"),
        new Caliber(".223 Rem (5,56×45 мм)"),
        new Caliber(".243 Win (6,2×52 мм)"),
        new Caliber("7,62×39"),
        new Caliber(".308 Win (7,62×51 мм)"),
        new Caliber("7,62x54R"),
        new Caliber("30-06 Sprg (7,62×63 мм)"),
        new Caliber(".300 Win Mag (7,62×67 мм)"),
        new Caliber(".338 LM"),
        new Caliber("9,3×62 мм"),
        new Caliber("9,3х74 мм"),
        new Caliber("5,45x39"),
        new Caliber("17 HMR"),
        new Caliber("12/70"),
        new Caliber("12/76"),
        new Caliber("12/89"),
        new Caliber("20/70"),
        new Caliber("20/76"),
        new Caliber("28/70"),
        new Caliber("28/76"),
        new Caliber("32/70"),
        new Caliber("32/76"),
        new Caliber("410/70"),
        new Caliber("410/76")
    };

    
    /// <summary>
    /// Добавляет дефолтные калибры, если их нет в БД
    /// </summary>
    /// <param name="context"></param>
    public static async Task InitializeAsync(ApplicationContext context)
    {
        if (!context.Calibers.Any())
        {
            await context.Calibers.AddRangeAsync(Calibers);
            await context.SaveChangesAsync();
        }
    }
}