namespace Portfolio.Application.Contact;

/// <summary>Настройки формы обратной связи (секция «Contact» в appsettings).</summary>
public sealed class ContactOptions
{
    public const string SectionName = "Contact";

    /// <summary>Окно, в котором считается количество сообщений от одного отправителя.</summary>
    public int FloodWindowMinutes { get; set; } = 60;

    /// <summary>Сколько сообщений разрешено за окно.</summary>
    public int MaxMessagesPerWindow { get; set; } = 5;

    /// <summary>
    /// Соль для хеширования IP. Без неё хеш можно подобрать перебором всех адресов.
    /// В продакшене задаётся переменной окружения Contact__IpHashSalt.
    /// </summary>
    public string IpHashSalt { get; set; } = string.Empty;
}
