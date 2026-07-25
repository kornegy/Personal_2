using Portfolio.Application.Abstractions;
using Portfolio.Application.Mapping;
using Portfolio.Shared.Contracts;

namespace Portfolio.Application.Services;

/// <inheritdoc cref="IPortfolioService" />
internal sealed class PortfolioService(IPortfolioRepository repository, TimeProvider timeProvider) : IPortfolioService
{
    public async Task<ProfileDto?> GetProfileAsync(string languageCode, CancellationToken cancellationToken = default)
    {
        var language = Languages.Normalize(languageCode);
        var profile = await repository.GetProfileAsync(language, cancellationToken);
        return profile?.ToDto(timeProvider.GetUtcNow());
    }

    public async Task<IReadOnlyList<SkillCategoryDto>> GetSkillsAsync(string languageCode, CancellationToken cancellationToken = default)
    {
        var categories = await repository.GetSkillCategoriesAsync(Languages.Normalize(languageCode), cancellationToken);
        return categories.Select(category => category.ToDto()).ToList();
    }

    public async Task<IReadOnlyList<ProjectDto>> GetProjectsAsync(string languageCode, CancellationToken cancellationToken = default)
    {
        var projects = await repository.GetProjectsAsync(Languages.Normalize(languageCode), cancellationToken);
        return projects.Select(project => project.ToDto()).ToList();
    }

    public async Task<IReadOnlyList<ExperienceDto>> GetExperienceAsync(string languageCode, CancellationToken cancellationToken = default)
    {
        var experiences = await repository.GetExperiencesAsync(Languages.Normalize(languageCode), cancellationToken);
        return experiences.Select(experience => experience.ToDto()).ToList();
    }
}
