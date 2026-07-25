using Portfolio.Shared.Contracts;

namespace Portfolio.Application.Abstractions;

/// <summary>Сценарии чтения содержимого сайта.</summary>
public interface IPortfolioService
{
    Task<ProfileDto?> GetProfileAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SkillCategoryDto>> GetSkillsAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProjectDto>> GetProjectsAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ExperienceDto>> GetExperienceAsync(CancellationToken cancellationToken = default);
}
