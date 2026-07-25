namespace Portfolio.Api.Security;

/// <summary>Настройки защищённого соединения (секция «Security» в appsettings).</summary>
public sealed class SecurityOptions
{
    public const string SectionName = "Security";

    /// <summary>
    /// Порт, на который перенаправляются запросы по http.
    /// null — порт определяется автоматически из адресов, на которых слушает сервер.
    /// В продакшене за прокси обычно 443.
    /// </summary>
    public int? HttpsPort { get; set; }

    /// <summary>
    /// Адреса reverse proxy (nginx, Cloudflare), которым можно доверять заголовки
    /// X-Forwarded-For и X-Forwarded-Proto. Пустой список — заголовки не читаются вовсе.
    ///
    /// Доверять всем подряд нельзя: тогда любой посетитель сможет подделать свой IP
    /// и обойти ограничение частоты запросов.
    /// </summary>
    public IList<string> KnownProxies { get; set; } = [];
}
