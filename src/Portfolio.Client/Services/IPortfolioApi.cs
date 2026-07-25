using Portfolio.Shared.Contracts;

namespace Portfolio.Client.Services;

/// <summary>Единая точка обращения к серверу из интерфейса.</summary>
public interface IPortfolioApi
{
    Task<ProfileDto?> GetProfileAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SkillCategoryDto>> GetSkillsAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProjectDto>> GetProjectsAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ExperienceDto>> GetExperienceAsync(CancellationToken cancellationToken = default);

    Task<ContactResult> SendContactAsync(ContactRequest request, CancellationToken cancellationToken = default);
}
