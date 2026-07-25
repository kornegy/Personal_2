using Microsoft.JSInterop;
using Portfolio.Shared.Contracts;

namespace Portfolio.Client.Localization;

/// <summary>
/// Текущий язык сайта. Выбор сохраняется в браузере, поэтому при следующем заходе
/// посетитель сразу видит нужную версию. Компоненты подписываются на <see cref="Changed"/>.
/// </summary>
public sealed class LanguageState(IJSRuntime jsRuntime)
{
    public string Current { get; private set; } = Languages.Default;

    public UiStrings Strings { get; private set; } = UiStringsCatalog.For(Languages.Default);

    /// <summary>Срабатывает после смены языка.</summary>
    public event Action? Changed;

    /// <summary>
    /// Определяет язык при старте: сначала сохранённый выбор, затем язык браузера.
    /// Вызывается один раз из Program.cs до отрисовки интерфейса.
    /// </summary>
    public async Task InitializeAsync()
    {
        var stored = await SafeInvokeAsync<string?>("portfolioApp.getStoredLanguage");
        var browser = await SafeInvokeAsync<string?>("portfolioApp.getBrowserLanguage");

        await ApplyAsync(Languages.Normalize(stored ?? browser), persist: false);
    }

    public async Task SetAsync(string languageCode)
    {
        var normalized = Languages.Normalize(languageCode);
        if (normalized == Current)
        {
            return;
        }

        await ApplyAsync(normalized, persist: true);
        Changed?.Invoke();
    }

    private async Task ApplyAsync(string languageCode, bool persist)
    {
        Current = languageCode;
        Strings = UiStringsCatalog.For(languageCode);

        // Атрибут lang и описание страницы важны для поисковиков и скринридеров.
        await SafeInvokeVoidAsync("portfolioApp.setDocumentLanguage", languageCode);
        await SafeInvokeVoidAsync("portfolioApp.setMetaDescription", Strings.MetaDescription);

        if (persist)
        {
            await SafeInvokeVoidAsync("portfolioApp.storeLanguage", languageCode);
        }
    }

    // Приватный режим браузера может запрещать localStorage — сайт не должен из-за этого падать.
    private async Task<T?> SafeInvokeAsync<T>(string identifier, params object?[] args)
    {
        try
        {
            return await jsRuntime.InvokeAsync<T>(identifier, args);
        }
        catch (JSException)
        {
            return default;
        }
    }

    private async Task SafeInvokeVoidAsync(string identifier, params object?[] args)
    {
        try
        {
            await jsRuntime.InvokeVoidAsync(identifier, args);
        }
        catch (JSException)
        {
            // Ничего страшного: язык всё равно применился в интерфейсе.
        }
    }
}
