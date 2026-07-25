using Portfolio.Shared.Contracts;

namespace Portfolio.Application.Abstractions;

/// <summary>Приём сообщений из формы обратной связи.</summary>
public interface IContactService
{
    /// <param name="request">Данные формы (уже прошли проверку атрибутов).</param>
    /// <param name="senderIp">IP отправителя — сохраняется только в виде хеша.</param>
    Task<ContactResult> SubmitAsync(ContactRequest request, string senderIp, CancellationToken cancellationToken = default);
}
