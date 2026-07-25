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
    public Task<ProfileDto?> GetProfileAsync(string languageCode, CancellationToken cancellationToken = default) =>
        GetAsync<ProfileDto>("api/profile", languageCode, cancellationToken);

    public async Task<IReadOnlyList<SkillCategoryDto>> GetSkillsAsync(string languageCode, CancellationToken cancellationToken = default) =>
        await GetAsync<List<SkillCategoryDto>>("api/skills", languageCode, cancellationToken) ?? [];

    public async Task<IReadOnlyList<ProjectDto>> GetProjectsAsync(string languageCode, CancellationToken cancellationToken = default) =>
        await GetAsync<List<ProjectDto>>("api/projects", languageCode, cancellationToken) ?? [];

    public async Task<IReadOnlyList<ExperienceDto>> GetExperienceAsync(string languageCode, CancellationToken cancellationToken = default) =>
        await GetAsync<List<ExperienceDto>>("api/experience", languageCode, cancellationToken) ?? [];

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
                return ContactResult.Failed(ContactResultCodes.RateLimited);
            }

            logger.LogError("Форма не отправлена, код ответа {StatusCode}", (int)response.StatusCode);
            return ContactResult.Failed(ContactResultCodes.Failed);
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException)
        {
            logger.LogError(exception, "Сеть недоступна при отправке формы");
            return ContactResult.Failed(ContactResultCodes.NetworkError);
        }
    }

    private async Task<T?> GetAsync<T>(string path, string languageCode, CancellationToken cancellationToken)
    {
        var url = $"{path}?lang={Uri.EscapeDataString(Languages.Normalize(languageCode))}";

        try
        {
            return await httpClient.GetFromJsonAsync<T>(url, cancellationToken);
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException or NotSupportedException)
        {
            logger.LogError(exception, "Не удалось загрузить {Path}", url);
            return default;
        }
    }
}
