using Microsoft.AspNetCore.Components;
using Portfolio.Client.Localization;

namespace Portfolio.Client.Components;

/// <summary>
/// База для компонентов с текстом: даёт доступ к подписям через <c>T</c>
/// и сама перерисовывает компонент при смене языка.
/// </summary>
public abstract class LocalizedComponentBase : ComponentBase, IDisposable
{
    [Inject] protected LanguageState Language { get; set; } = default!;

    /// <summary>Подписи текущего языка.</summary>
    protected UiStrings T => Language.Strings;

    protected override void OnInitialized()
    {
        Language.Changed += HandleLanguageChanged;
        base.OnInitialized();
    }

    /// <summary>Вызывается при смене языка до перерисовки — например, чтобы перезагрузить данные.</summary>
    protected virtual Task OnLanguageChangedAsync() => Task.CompletedTask;

    private void HandleLanguageChanged() =>
        _ = InvokeAsync(async () =>
        {
            await OnLanguageChangedAsync();
            StateHasChanged();
        });

    public virtual void Dispose()
    {
        Language.Changed -= HandleLanguageChanged;
        GC.SuppressFinalize(this);
    }
}
