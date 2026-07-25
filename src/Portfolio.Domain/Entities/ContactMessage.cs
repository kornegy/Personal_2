namespace Portfolio.Domain.Entities;

/// <summary>Сообщение из формы обратной связи.</summary>
public class ContactMessage
{
    public int Id { get; set; }

    public string SenderName { get; set; } = string.Empty;

    public string SenderEmail { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    /// <summary>
    /// Момент получения в UTC. Именно DateTime, а не DateTimeOffset:
    /// SQLite не умеет сравнивать DateTimeOffset в запросах, а проверка лимита
    /// сообщений как раз сравнивает даты.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; }

    /// <summary>
    /// SHA-256 от IP-адреса отправителя. Хранится в виде хеша, чтобы иметь защиту от спама,
    /// но не хранить персональные данные в открытом виде.
    /// </summary>
    public string SenderIpHash { get; set; } = string.Empty;
}
