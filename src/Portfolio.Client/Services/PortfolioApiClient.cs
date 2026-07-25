using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Portfolio.Shared.Contracts;

namespace Portfolio.Client.Services;

/// <summary>
/// HTTP-клиент к API. Сетевые ошибки не роняют страницу: секция просто останется пустой,
/// а причина уйдёт в консоль браузера.
/// </summary>
public sealed class PortfolioApiClient(HttpClient httpClient, ILogger<PortfolioApiClient> logger) : IPortfolioApi
{
    public Task<ProfileDto?> GetProfileAsync(CancellationToken cancellationToken = default) =>
        GetAsync<ProfileDto>("api/profile", cancellationToken);

    public async Task<IReadOnlyList<SkillCategoryDto>> GetSkillsAsync(CancellationToken cancellationToken = default) =>
        await GetAsync<List<SkillCategoryDto>>("api/skills", cancellationToken) ?? [];

    public async Task<IReadOnlyList<ProjectDto>> GetProjectsAsync(CancellationToken cancellationToken = default) =>
        await GetAsync<List<ProjectDto>>("api/projects", cancellationToken) ?? [];

    public async Task<IReadOnlyList<ExperienceDto>> GetExperienceAsync(CancellationToken cancellationToken = default) =>
        await GetAsync<List<ExperienceDto>>("api/experience", cancellationToken) ?? [];

    public async Task<ContactResult> SendContactAsync(ContactRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/contact", request, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ContactResult>(cancellationToken);
                return result ?? ContactResult.Accepted();
            }

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                return ContactResult.Failed("Слишком много сообщений подряд. Попробуйте позже или напишите на почту.");
            }

            logger.LogError("Форма не отправлена, код ответа {StatusCode}", (int)response.StatusCode);
            return ContactResult.Failed("Не получилось отправить сообщение. Попробуйте ещё раз.");
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
        {
            logger.LogError(exception, "Сеть недоступна при отправке формы");
            return ContactResult.Failed("Нет связи с сервером. Проверьте подключение и попробуйте снова.");
        }
    }

    private async Task<T?> GetAsync<T>(string path, CancellationToken cancellationToken)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<T>(path, cancellationToken);
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException or NotSupportedException)
        {
            logger.LogError(exception, "Не удалось загрузить {Path}", path);
            return default;
        }
    }
}
