namespace Portfolio.Shared.Contracts;

/// <summary>
/// Ответ на отправку формы. Сервер возвращает код, а не готовый текст:
/// текст зависит от выбранного языка и подставляется на клиенте.
/// </summary>
public record ContactResult(bool Success, string Code)
{
    public static ContactResult Accepted() => new(true, ContactResultCodes.Accepted);

    public static ContactResult Failed(string code) => new(false, code);
}

/// <summary>Возможные исходы отправки формы.</summary>
public static class ContactResultCodes
{
    public const string Accepted = "accepted";

    public const string RateLimited = "rate_limited";

    public const string Failed = "failed";

    public const string NetworkError = "network_error";
}
