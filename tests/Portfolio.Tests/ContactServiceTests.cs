using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Portfolio.Application.Contact;
using Portfolio.Application.Services;
using Portfolio.Shared.Contracts;
using Portfolio.Tests.Common;

namespace Portfolio.Tests;

public class ContactServiceTests
{
    private static readonly DateTimeOffset Now = new(2026, 7, 25, 12, 0, 0, TimeSpan.Zero);

    private static ContactRequest ValidRequest() => new()
    {
        Name = "Иван",
        Email = "ivan@example.com",
        Subject = "Лендинг",
        Message = "Нужен лендинг для студии, сроки — месяц."
    };

    private static ContactService CreateService(FakeContactMessageRepository repository, int maxPerWindow = 5)
    {
        var options = Options.Create(new ContactOptions
        {
            FloodWindowMinutes = 60,
            MaxMessagesPerWindow = maxPerWindow,
            IpHashSalt = "test-salt"
        });

        return new ContactService(repository, options, new StubTimeProvider(Now), NullLogger<ContactService>.Instance);
    }

    [Fact]
    public async Task SubmitAsync_КорректныеДанные_СохраняетСообщение()
    {
        var repository = new FakeContactMessageRepository();
        var service = CreateService(repository);

        var result = await service.SubmitAsync(ValidRequest(), "203.0.113.10");

        result.Success.Should().BeTrue();
        repository.Messages.Should().ContainSingle();
        repository.Messages[0].SenderName.Should().Be("Иван");
    }

    [Fact]
    public async Task SubmitAsync_НеСохраняетIpВОткрытомВиде()
    {
        var repository = new FakeContactMessageRepository();
        var service = CreateService(repository);

        await service.SubmitAsync(ValidRequest(), "203.0.113.10");

        repository.Messages[0].SenderIpHash.Should().NotContain("203.0.113.10");
        repository.Messages[0].SenderIpHash.Should().HaveLength(64);
    }

    [Fact]
    public async Task SubmitAsync_ЗаполненHoneypot_НичегоНеСохраняет()
    {
        var repository = new FakeContactMessageRepository();
        var service = CreateService(repository);

        var request = ValidRequest();
        request.Website = "https://spam.example";

        var result = await service.SubmitAsync(request, "203.0.113.10");

        // Боту отвечаем как при успехе, но сообщение не сохраняется.
        result.Success.Should().BeTrue();
        repository.Messages.Should().BeEmpty();
    }

    [Fact]
    public async Task SubmitAsync_ПревышенЛимит_ОтклоняетСообщение()
    {
        var repository = new FakeContactMessageRepository();
        var service = CreateService(repository, maxPerWindow: 2);

        await service.SubmitAsync(ValidRequest(), "203.0.113.10");
        await service.SubmitAsync(ValidRequest(), "203.0.113.10");
        var third = await service.SubmitAsync(ValidRequest(), "203.0.113.10");

        third.Success.Should().BeFalse();
        repository.Messages.Should().HaveCount(2);
    }

    [Fact]
    public async Task SubmitAsync_ЛимитСчитаетсяОтдельноПоКаждомуОтправителю()
    {
        var repository = new FakeContactMessageRepository();
        var service = CreateService(repository, maxPerWindow: 1);

        await service.SubmitAsync(ValidRequest(), "203.0.113.10");
        var other = await service.SubmitAsync(ValidRequest(), "198.51.100.7");

        other.Success.Should().BeTrue();
        repository.Messages.Should().HaveCount(2);
    }
}
