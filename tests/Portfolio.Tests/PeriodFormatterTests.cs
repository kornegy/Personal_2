using FluentAssertions;
using Portfolio.Application.Common;

namespace Portfolio.Tests;

public class PeriodFormatterTests
{
    [Fact]
    public void Format_ЗакрытыйПериод_ВозвращаетОбеДаты()
    {
        var result = PeriodFormatter.Format(new DateOnly(2022, 3, 1), new DateOnly(2023, 5, 1));

        result.Should().Be("Март 2022 — Май 2023");
    }

    [Fact]
    public void Format_БезДатыОкончания_ПоказываетНастоящееВремя()
    {
        var result = PeriodFormatter.Format(new DateOnly(2023, 6, 1), null);

        result.Should().Be("Июнь 2023 — настоящее время");
    }

    [Theory]
    [InlineData(2022, 2026, 4)]
    [InlineData(2026, 2026, 1)] // меньше года всё равно показываем как год
    public void YearsSince_СчитаетПолныеГоды(int startYear, int currentYear, int expected)
    {
        var now = new DateTimeOffset(currentYear, 7, 1, 0, 0, 0, TimeSpan.Zero);

        PeriodFormatter.YearsSince(startYear, now).Should().Be(expected);
    }
}
