using Portfolio.Shared.Contracts;

namespace Portfolio.Client.Services;

/// <summary>Единая точка обращения к серверу из интерфейса.</summary>
public interface IPortfolioApi
{
    Task<ProfileDto?> GetProfileAsync(string languageCode, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SkillCategoryDto>> GetSkillsAsync(string languageCode, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProjectDto>> GetProjectsAsync(string languageCode, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ExperienceDto>> GetExperienceAsync(string languageCode, CancellationToken cancellationToken = default);

    Task<ContactResult> SendContactAsync(ContactRequest request, CancellationToken cancellationToken = default);
}
