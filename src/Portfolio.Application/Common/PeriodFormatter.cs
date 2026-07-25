using Portfolio.Shared.Contracts;

namespace Portfolio.Application.Common;

/// <summary>
/// Форматирование периодов работы. Названия месяцев заданы явно, а не через CultureInfo:
/// приложение собирается с InvariantGlobalization, и так результат не зависит от локали сервера.
/// </summary>
public static class PeriodFormatter
{
    private static readonly string[] UkrainianMonths =
    [
        "січень", "лютий", "березень", "квітень", "травень", "червень",
        "липень", "серпень", "вересень", "жовтень", "листопад", "грудень"
    ];

    private static readonly string[] EnglishMonths =
    [
        "january", "february", "march", "april", "may", "june",
        "july", "august", "september", "october", "november", "december"
    ];

    private const string UkrainianPresent = "дотепер";

    private const string EnglishPresent = "Present";

    /// <summary>Возвращает строку вида «Березень 2022 — дотепер» или «March 2022 — Present».</summary>
    public static string Format(DateOnly start, DateOnly? end, string languageCode)
    {
        var isUkrainian = languageCode == Languages.Ukrainian;
        var months = isUkrainian ? UkrainianMonths : EnglishMonths;

        var from = FormatSingle(start, months);
        var to = end.HasValue
            ? FormatSingle(end.Value, months)
            : isUkrainian ? UkrainianPresent : EnglishPresent;

        return $"{from} — {to}";
    }

    private static string FormatSingle(DateOnly date, string[] months)
    {
        var month = months[date.Month - 1];
        return $"{char.ToUpperInvariant(month[0])}{month[1..]} {date.Year}";
    }
}
