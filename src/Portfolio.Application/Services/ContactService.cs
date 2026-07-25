using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Portfolio.Application.Abstractions;
using Portfolio.Application.Contact;
using Portfolio.Domain.Entities;
using Portfolio.Shared.Contracts;

namespace Portfolio.Application.Services;

/// <inheritdoc cref="IContactService" />
internal sealed class ContactService(
    IContactMessageRepository repository,
    IOptions<ContactOptions> options,
    TimeProvider timeProvider,
    ILogger<ContactService> logger) : IContactService
{
    private readonly ContactOptions _options = options.Value;

    public async Task<ContactResult> SubmitAsync(
        ContactRequest request,
        string senderIp,
        CancellationToken cancellationToken = default)
    {
        // Honeypot заполнен — это бот. Отвечаем как при успехе, чтобы не подсказывать,
        // по какому признаку сработала защита, но ничего не сохраняем.
        if (!string.IsNullOrWhiteSpace(request.Website))
        {
            logger.LogWarning("Сообщение отклонено: заполнено скрытое поле honeypot");
            return ContactResult.Accepted();
        }

        var now = timeProvider.GetUtcNow();
        var ipHash = SenderIpHasher.Hash(senderIp, _options.IpHashSalt);
        var windowStart = now.AddMinutes(-_options.FloodWindowMinutes);

        var recentCount = await repository.CountSinceAsync(ipHash, windowStart, cancellationToken);
        if (recentCount >= _options.MaxMessagesPerWindow)
        {
            logger.LogWarning("Сообщение отклонено: превышен лимит {Limit} сообщений за {Window} мин.",
                _options.MaxMessagesPerWindow, _options.FloodWindowMinutes);

            return ContactResult.Failed("Вы отправили слишком много сообщений. Попробуйте позже или напишите на почту.");
        }

        var message = new ContactMessage
        {
            SenderName = request.Name.Trim(),
            SenderEmail = request.Email.Trim(),
            Subject = request.Subject.Trim(),
            Body = request.Message.Trim(),
            CreatedAtUtc = now,
            SenderIpHash = ipHash
        };

        await repository.AddAsync(message, cancellationToken);
        logger.LogInformation("Сохранено новое сообщение из формы обратной связи (Id {Id})", message.Id);

        return ContactResult.Accepted();
    }
}
