using Portfolio.Domain.Entities;

namespace Portfolio.Application.Abstractions;

/// <summary>
/// Доступ к содержимому сайта. Реализация живёт в слое Infrastructure.
/// Контент хранится по языкам, поэтому код языка обязателен в каждом запросе.
/// </summary>
public interface IPortfolioRepository
{
    Task<Profile?> GetProfileAsync(string languageCode, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SkillCategory>> GetSkillCategoriesAsync(string languageCode, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Project>> GetProjectsAsync(string languageCode, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Experience>> GetExperiencesAsync(string languageCode, CancellationToken cancellationToken = default);
}
