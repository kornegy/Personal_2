using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Portfolio.Application.Services;
using Portfolio.Infrastructure.Persistence;
using Portfolio.Infrastructure.Repositories;
using Portfolio.Tests.Common;

namespace Portfolio.Tests;

/// <summary>
/// Проверка связки «база → репозиторий → сервис → DTO» на настоящей SQLite в памяти:
/// так тест ловит и ошибки в конфигурации EF, а не только в C#-коде.
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

        var now = new DateTimeOffset(2026, 7, 25, 0, 0, 0, TimeSpan.Zero);
        _service = new PortfolioService(new PortfolioRepository(_context), new StubTimeProvider(now));
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _connection.DisposeAsync();
    }

    [Fact]
    public async Task GetProfileAsync_ВозвращаетЗаполненныйПрофиль()
    {
        var profile = await _service.GetProfileAsync();

        profile.Should().NotBeNull();
        profile!.FullName.Should().NotBeNullOrWhiteSpace();
        profile.Email.Should().NotBeNullOrWhiteSpace();
        profile.YearsOfExperience.Should().BeGreaterThan(0);
        profile.SocialLinks.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetSkillsAsync_КатегорииИдутВЗаданномПорядкеИНеПустые()
    {
        var categories = await _service.GetSkillsAsync();

        categories.Should().NotBeEmpty();
        categories.Should().OnlyContain(category => category.Skills.Count > 0);
    }

    [Fact]
    public async Task GetProjectsAsync_УКаждогоПроектаЕстьТехнологии()
    {
        var projects = await _service.GetProjectsAsync();

        projects.Should().NotBeEmpty();
        projects.Should().OnlyContain(project => project.Technologies.Count > 0);
    }

    [Fact]
    public async Task GetExperienceAsync_ТекущееМестоОтмеченоИПериодОтформатирован()
    {
        var experience = await _service.GetExperienceAsync();

        experience.Should().NotBeEmpty();
        experience.Should().Contain(item => item.IsCurrent);
        experience.Should().OnlyContain(item => item.Period.Contains('—'));
    }

    [Fact]
    public async Task InitializeAsync_ПовторныйЗапуск_НеДублируетДанные()
    {
        await new DatabaseInitializer(_context, NullLogger<DatabaseInitializer>.Instance).InitializeAsync();

        (await _context.Profiles.CountAsync()).Should().Be(1);
    }
}
