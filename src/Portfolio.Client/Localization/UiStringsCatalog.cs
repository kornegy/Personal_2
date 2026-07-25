using Portfolio.Shared.Contracts;

namespace Portfolio.Client.Localization;

/// <summary>Выдаёт набор подписей по коду языка.</summary>
public static class UiStringsCatalog
{
    public static UiStrings For(string languageCode) =>
        Languages.Normalize(languageCode) == Languages.English
            ? EnglishStrings.Value
            : RussianStrings.Value;
}
