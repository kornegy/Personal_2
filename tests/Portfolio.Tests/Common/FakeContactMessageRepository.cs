using Portfolio.Application.Abstractions;
using Portfolio.Domain.Entities;

namespace Portfolio.Tests.Common;

/// <summary>Хранилище сообщений в памяти. Заменяет базу в тестах бизнес-логики.</summary>
internal sealed class FakeContactMessageRepository : IContactMessageRepository
{
    public List<ContactMessage> Messages { get; } = [];

    public Task AddAsync(ContactMessage message, CancellationToken cancellationToken = default)
    {
        Messages.Add(message);
        return Task.CompletedTask;
    }

    public Task<int> CountSinceAsync(string senderIpHash, DateTimeOffset since, CancellationToken cancellationToken = default) =>
        Task.FromResult(Messages.Count(m => m.SenderIpHash == senderIpHash && m.CreatedAtUtc >= since));
}
