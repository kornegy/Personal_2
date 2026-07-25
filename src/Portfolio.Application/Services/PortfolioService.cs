using Portfolio.Application.Abstractions;
using Portfolio.Application.Mapping;
using Portfolio.Shared.Contracts;

namespace Portfolio.Application.Services;

/// <inheritdoc cref="IPortfolioService" />
internal sealed class PortfolioService(IPortfolioRepository repository, TimeProvider timeProvider) : IPortfolioService
{
    public async Task<ProfileDto?> GetProfileAsync(CancellationToken cancellationToken = default)
    {
        var profile = await repository.GetProfileAsync(cancellationToken);
        return profile?.ToDto(timeProvider.GetUtcNow());
    }

    public async Task<IReadOnlyList<SkillCategoryDto>> GetSkillsAsync(CancellationToken cancellationToken = default)
    {
        var categories = await repository.GetSkillCategoriesAsync(cancellationToken);
        return categories.Select(category => category.ToDto()).ToList();
    }

    public async Task<IReadOnlyList<ProjectDto>> GetProjectsAsync(CancellationToken cancellationToken = default)
    {
        var projects = await repository.GetProjectsAsync(cancellationToken);
        return projects.Select(project => project.ToDto()).ToList();
    }

    public async Task<IReadOnlyList<ExperienceDto>> GetExperienceAsync(CancellationToken cancellationToken = default)
    {
        var experiences = await repository.GetExperiencesAsync(cancellationToken);
        return experiences.Select(experience => experience.ToDto()).ToList();
    }
}
