using Portfolio.Domain.Entities;

namespace Portfolio.Application.Abstractions;

/// <summary>Доступ к содержимому сайта. Реализация живёт в слое Infrastructure.</summary>
public interface IPortfolioRepository
{
    Task<Profile?> GetProfileAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SkillCategory>> GetSkillCategoriesAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Project>> GetProjectsAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Experience>> GetExperiencesAsync(CancellationToken cancellationToken = default);
}
