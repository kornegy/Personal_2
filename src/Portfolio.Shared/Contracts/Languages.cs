namespace Portfolio.Shared.Contracts;

/// <summary>
/// Языки сайта. Список один на клиент и сервер, поэтому расхождений быть не может.
/// Чтобы добавить третий язык, достаточно дописать код сюда и добавить пакет контента.
/// </summary>
public static class Languages
{
    public const string Russian = "ru";

    public const string English = "en";

    public const string Default = Russian;

    public static readonly IReadOnlyList<string> All = [Russian, English];

    /// <summary>Приводит произвольную строку к поддерживаемому коду языка.</summary>
    public static string Normalize(string? code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return Default;
        }

        var normalized = code.Trim().ToLowerInvariant();
        return All.Contains(normalized) ? normalized : Default;
    }
}
