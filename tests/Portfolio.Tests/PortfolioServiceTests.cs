using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Portfolio.Application.Services;
using Portfolio.Infrastructure.Persistence;
using Portfolio.Infrastructure.Repositories;
using Portfolio.Shared.Contracts;
using Portfolio.Tests.Common;

namespace Portfolio.Tests;

/// <summary>
/// Проверка связки «миграции → база → репозиторий → сервис → DTO» на настоящей SQLite в памяти:
/// так тест ловит и ошибки в миграциях с конфигурацией EF, а не только в C#-коде.
/// </summary>
public class PortfolioServiceTests : IAsyncLifetime
{
    private readonly SqliteConnection _connection = new("Filename=:memory:");
    private PortfolioDbContext _context = null!;
    private PortfolioService _service = null!;

    public async Task InitializeAsync()
    {
        await _connection.OpenAsync();

        var options = new DbContextOptionsBuilder<PortfolioDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new PortfolioDbContext(options);
        await new DatabaseInitializer(_context, NullLogger<DatabaseInitializer>.Instance).InitializeAsync();

        _service = new PortfolioService(new PortfolioRepository(_context));
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _connection.DisposeAsync();
    }

    [Fact]
    public async Task InitializeAsync_ПрименяетМиграцииИСоздаётТаблицы()
    {
        var applied = await _context.Database.GetAppliedMigrationsAsync();

        applied.Should().NotBeEmpty();
        (await _context.Database.GetPendingMigrationsAsync()).Should().BeEmpty();
    }

    [Theory]
    [InlineData(Languages.Russian)]
    [InlineData(Languages.English)]
    public async Task GetProfileAsync_ЕстьПрофильНаКаждомЯзыке(string language)
    {
        var profile = await _service.GetProfileAsync(language);

        profile.Should().NotBeNull();
        profile!.FullName.Should().NotBeNullOrWhiteSpace();
        profile.Email.Should().NotBeNullOrWhiteSpace();
        profile.YearsOfExperience.Should().BeGreaterThan(0);
        profile.SocialLinks.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetProfileAsync_РусскаяИАнглийскаяВерсииОтличаются()
    {
        var russian = await _service.GetProfileAsync(Languages.Russian);
        var english = await _service.GetProfileAsync(Languages.English);

        russian!.Title.Should().NotBe(english!.Title);
    }

    [Fact]
    public async Task GetProfileAsync_НеизвестныйЯзык_ВозвращаетВерсиюПоУмолчанию()
    {
        var fallback = await _service.GetProfileAsync("zz");
        var russian = await _service.GetProfileAsync(Languages.Russian);

        fallback!.Title.Should().Be(russian!.Title);
    }

    [Theory]
    [InlineData(Languages.Russian)]
    [InlineData(Languages.English)]
    public async Task GetSkillsAsync_КатегорииНеПустые(string language)
    {
        var categories = await _service.GetSkillsAsync(language);

        categories.Should().NotBeEmpty();
        categories.Should().OnlyContain(category => category.Skills.Count > 0);
    }

    [Fact]
    public async Task GetProjectsAsync_ОдинаковоеЧислоПроектовНаОбоихЯзыках()
    {
        var russian = await _service.GetProjectsAsync(Languages.Russian);
        var english = await _service.GetProjectsAsync(Languages.English);

        russian.Should().NotBeEmpty();
        english.Should().HaveCount(russian.Count);
        russian.Should().OnlyContain(project => project.Technologies.Count > 0);
    }

    [Fact]
    public async Task GetExperienceAsync_ПериодПереводитсяВместеСЯзыком()
    {
        var russian = await _service.GetExperienceAsync(Languages.Russian);
        var english = await _service.GetExperienceAsync(Languages.English);

        russian.Should().NotBeEmpty();
        english.Should().HaveCount(russian.Count);
        russian.Should().Contain(item => item.Period.Contains("Сентябрь"));
        english.Should().Contain(item => item.Period.Contains("September"));
    }

    [Fact]
    public async Task GetExperienceAsync_ЗавершённоеМестоРаботыНеПомеченоКакТекущее()
    {
        var experience = await _service.GetExperienceAsync(Languages.Russian);

        experience.Should().OnlyContain(item => item.Highlights.Count > 0);
        experience.Should().NotContain(item => item.IsCurrent);
    }

    [Fact]
    public async Task InitializeAsync_ПовторныйЗапуск_НеДублируетДанные()
    {
        await new DatabaseInitializer(_context, NullLogger<DatabaseInitializer>.Instance).InitializeAsync();

        (await _context.Profiles.CountAsync()).Should().Be(Languages.All.Count);
    }
}
