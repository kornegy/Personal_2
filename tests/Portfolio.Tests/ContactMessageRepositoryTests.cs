using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Persistence;
using Portfolio.Infrastructure.Repositories;

namespace Portfolio.Tests;

/// <summary>
/// Проверка защиты от флуда на настоящей SQLite.
/// Тест с подставным хранилищем такую ошибку пропустил бы: сравнение дат
/// ломается не в C#, а при переводе запроса в SQL.
/// </summary>
public class ContactMessageRepositoryTests : IAsyncLifetime
{
    private static readonly DateTime NowUtc = new(2026, 7, 25, 12, 0, 0, DateTimeKind.Utc);

    private readonly SqliteConnection _connection = new("Filename=:memory:");
    private PortfolioDbContext _context = null!;
    private ContactMessageRepository _repository = null!;

    public async Task InitializeAsync()
    {
        await _connection.OpenAsync();

        var options = new DbContextOptionsBuilder<PortfolioDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new PortfolioDbContext(options);
        await new DatabaseInitializer(_context, NullLogger<DatabaseInitializer>.Instance).InitializeAsync();

        _repository = new ContactMessageRepository(_context);
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _connection.DisposeAsync();
    }

    [Fact]
    public async Task CountSinceAsync_СчитаетТолькоСообщенияВнутриОкна()
    {
        await _repository.AddAsync(CreateMessage("hash-1", NowUtc.AddMinutes(-10)));
        await _repository.AddAsync(CreateMessage("hash-1", NowUtc.AddMinutes(-30)));
        await _repository.AddAsync(CreateMessage("hash-1", NowUtc.AddHours(-5)));   // за окном
        await _repository.AddAsync(CreateMessage("hash-2", NowUtc.AddMinutes(-5))); // другой отправитель

        var count = await _repository.CountSinceAsync("hash-1", NowUtc.AddHours(-1));

        count.Should().Be(2);
    }

    [Fact]
    public async Task AddAsync_СохраняетСообщение()
    {
        await _repository.AddAsync(CreateMessage("hash-3", NowUtc));

        (await _context.ContactMessages.CountAsync()).Should().Be(1);
    }

    private static ContactMessage CreateMessage(string ipHash, DateTime createdAtUtc) => new()
    {
        SenderName = "Иван",
        SenderEmail = "ivan@example.com",
        Subject = "Проект",
        Body = "Текст сообщения для проверки.",
        CreatedAtUtc = createdAtUtc,
        SenderIpHash = ipHash
    };
}
