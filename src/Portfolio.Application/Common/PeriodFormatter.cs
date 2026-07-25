namespace Portfolio.Application.Common;

/// <summary>
/// Форматирование периодов работы. Названия месяцев заданы явно, а не через CultureInfo:
/// приложение собирается с InvariantGlobalization, и так результат не зависит от локали сервера.
/// </summary>
public static class PeriodFormatter
{
    private static readonly string[] Months =
    [
        "январь", "февраль", "март", "апрель", "май", "июнь",
        "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь"
    ];

    private const string Present = "настоящее время";

    /// <summary>Возвращает строку вида «Март 2022 — настоящее время».</summary>
    public static string Format(DateOnly start, DateOnly? end)
    {
        var from = FormatSingle(start);
        var to = end.HasValue ? FormatSingle(end.Value) : Present;
        return $"{from} — {to}";
    }

    private static string FormatSingle(DateOnly date)
    {
        var month = Months[date.Month - 1];
        return $"{char.ToUpperInvariant(month[0])}{month[1..]} {date.Year}";
    }

    /// <summary>Полных лет опыта с начала карьеры, но не меньше единицы.</summary>
    public static int YearsSince(int careerStartYear, DateTimeOffset now)
    {
        var years = now.Year - careerStartYear;
        return years < 1 ? 1 : years;
    }
}
