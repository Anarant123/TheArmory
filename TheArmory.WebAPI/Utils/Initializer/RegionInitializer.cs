using TheArmory.Context;
using TheArmory.Domain.Models.Database;
using TheArmory.Domain.Models.Enums;

namespace TheArmory.Utils.Initializer;

public class RegionInitializer : Initializer
{
    public static readonly List<Region> Regions = new()
    {
        new Region("Республика Адыгея", 1),
        new Region("Республика Алтай", 4),
        new Region("Республика Башкортостан", 2),
        new Region("Республика Бурятия", 3),
        new Region("Республика Дагестан", 5),
        new Region("Республика Ингушетия", 6),
        new Region("Кабардино-Балкарская Республика", 7),
        new Region("Республика Калмыкия", 8),
        new Region("Республика Карачаево-Черкессия", 9),
        new Region("Республика Карелия", 10),
        new Region("Республика Коми", 11),
        new Region("Республика Крым", 91),
        new Region("Республика Марий Эл", 12),
        new Region("Республика Мордовия", 13),
        new Region("Республика Саха (Якутия)", 14),
        new Region("Республика Северная Осетия - Алания", 15),
        new Region("Республика Татарстан", 16),
        new Region("Республика Тыва", 17),
        new Region("Удмуртская Республика", 18),
        new Region("Республика Хакасия", 19),
        new Region("Чеченская Республика", 20),
        new Region("Чувашская Республика", 21),
        new Region("Алтайский край", 22),
        new Region("Краснодарский край", 23),
        new Region("Красноярский край", 24),
        new Region("Приморский край", 25),
        new Region("Ставропольский край", 26),
        new Region("Хабаровский край", 27),
        new Region("Амурская область", 28),
        new Region("Архангельская область", 29),
        new Region("Астраханская область", 30),
        new Region("Белгородская область", 31),
        new Region("Брянская область", 32),
        new Region("Владимирская область", 33),
        new Region("Волгоградская область", 34),
        new Region("Вологодская область", 35),
        new Region("Воронежская область", 36),
        new Region("Ивановская область", 37),
        new Region("Иркутская область", 38),
        new Region("Калининградская область", 39),
        new Region("Калужская область", 40),
        new Region("Камчатский край", 41),
        new Region("Кемеровская область", 42),
        new Region("Кировская область", 43),
        new Region("Костромская область", 44),
        new Region("Курганская область", 45),
        new Region("Курская область", 46),
        new Region("Ленинградская область", 47),
        new Region("Липецкая область", 48),
        new Region("Магаданская область", 49),
        new Region("Московская область", 50),
        new Region("Мурманская область", 51),
        new Region("Нижегородская область", 52),
        new Region("Новгородская область", 53),
        new Region("Новосибирская область", 54),
        new Region("Омская область", 55),
        new Region("Оренбургская область", 56),
        new Region("Орловская область", 57),
        new Region("Пензенская область", 58),
        new Region("Пермский край", 59),
        new Region("Псковская область", 60),
        new Region("Ростовская область", 61),
        new Region("Рязанская область", 62),
        new Region("Самарская область", 63),
        new Region("Саратовская область", 64),
        new Region("Сахалинская область", 65),
        new Region("Свердловская область", 66),
        new Region("Смоленская область", 67),
        new Region("Тамбовская область", 68),
        new Region("Тверская область", 69),
        new Region("Томская область", 70),
        new Region("Тульская область", 71),
        new Region("Тюменская область", 72),
        new Region("Ульяновская область", 73),
        new Region("Челябинская область", 74),
        new Region("Забайкальский край", 75),
        new Region("Ярославская область", 76),
        new Region("г. Москва", 77),
        new Region("г. Санкт-Петербург", 78),
        new Region("Еврейская автономная область", 79),
        new Region("Ненецкий автономный округ", 83),
        new Region("Ханты-Мансийский автономный округ - Югра", 86),
        new Region("Чукотский автономный округ", 87),
        new Region("Ямало-Ненецкий автономный округ", 89),
        new Region("г. Севастополь", 92),
        new Region("Крым", 91) // Повторение в связи с некоторыми различиями в обозначении региона
    };

    /// <summary>
    /// Добавляет дефолтные роли, если их нет в БД
    /// </summary>
    /// <param name="context"></param>
    public static async Task InitializeAsync(ApplicationContext context)
    {
        if (!context.Regions.Any())
        {
            await context.Regions.AddRangeAsync(Regions);
            await context.SaveChangesAsync();
        }
    }
}