using FluentAssertions;
using Portfolio.Application.Common;
using Portfolio.Shared.Contracts;

namespace Portfolio.Tests;

public class PeriodFormatterTests
{
    [Fact]
    public void Format_ЗакрытыйПериод_ВозвращаетОбеДаты()
    {
        var result = PeriodFormatter.Format(new DateOnly(2022, 3, 1), new DateOnly(2023, 5, 1), Languages.Russian);

        result.Should().Be("Март 2022 — Май 2023");
    }

    [Fact]
    public void Format_БезДатыОкончания_ПоказываетНастоящееВремя()
    {
        var result = PeriodFormatter.Format(new DateOnly(2023, 6, 1), null, Languages.Russian);

        result.Should().Be("Июнь 2023 — настоящее время");
    }

    [Fact]
    public void Format_АнглийскийЯзык_ИспользуетАнглийскиеМесяцы()
    {
        var result = PeriodFormatter.Format(new DateOnly(2022, 3, 1), new DateOnly(2023, 5, 1), Languages.English);

        result.Should().Be("March 2022 — May 2023");
    }

    [Fact]
    public void Format_АнглийскийЯзыкБезОкончания_ПоказываетPresent()
    {
        var result = PeriodFormatter.Format(new DateOnly(2023, 6, 1), null, Languages.English);

        result.Should().Be("June 2023 — Present");
    }

}
