using Portfolio.Domain.Entities;

namespace Portfolio.Application.Abstractions;

/// <summary>Хранилище сообщений из формы обратной связи.</summary>
public interface IContactMessageRepository
{
    Task AddAsync(ContactMessage message, CancellationToken cancellationToken = default);

    /// <summary>Сколько сообщений пришло с этого отправителя начиная с указанного момента.</summary>
    Task<int> CountSinceAsync(string senderIpHash, DateTimeOffset since, CancellationToken cancellationToken = default);
}
