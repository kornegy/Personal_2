using Portfolio.Shared.Contracts;

namespace Portfolio.Application.Abstractions;

/// <summary>Сценарии чтения содержимого сайта на выбранном языке.</summary>
public interface IPortfolioService
{
    Task<ProfileDto?> GetProfileAsync(string languageCode, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SkillCategoryDto>> GetSkillsAsync(string languageCode, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProjectDto>> GetProjectsAsync(string languageCode, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ExperienceDto>> GetExperienceAsync(string languageCode, CancellationToken cancellationToken = default);
}
