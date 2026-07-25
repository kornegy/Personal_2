namespace Portfolio.Api.Security;

/// <summary>Настройки защищённого соединения (секция «Security» в appsettings).</summary>
public sealed class SecurityOptions
{
    public const string SectionName = "Security";

    /// <summary>
    /// Перенаправлять ли http на https силами приложения.
    ///
    /// Выключите, если TLS завершается на reverse proxy, а до приложения запрос
    /// доходит по http и доверенные прокси не настроены: иначе получится
    /// бесконечный цикл редиректов.
    /// </summary>
    public bool EnableHttpsRedirection { get; set; } = true;

    /// <summary>
    /// Порт, на который перенаправляются запросы по http.
    /// null — порт определяется автоматически из адресов, на которых слушает сервер.
    /// За прокси обычно 443.
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

    /// <summary>
    /// Доверять заголовкам X-Forwarded-* от любого адреса.
    ///
    /// Нужно на платформах вроде Fly.io или Railway, где адрес их прокси заранее
    /// неизвестен и меняется. Включать можно, только если приложение недоступно
    /// напрямую и весь трафик обязательно проходит через прокси платформы.
    /// </summary>
    public bool TrustAllProxies { get; set; }
}
