using Portfolio.Application.Common;
using Portfolio.Domain.Entities;
using Portfolio.Shared.Contracts;

namespace Portfolio.Application.Mapping;

/// <summary>
/// Преобразование сущностей в контракты API. Ручной маппинг вместо библиотеки:
/// объектов немного, зато нет магии и всё видно при чтении.
/// </summary>
internal static class PortfolioMappings
{
    public static ProfileDto ToDto(this Profile profile, DateTimeOffset now) => new(
        profile.FullName,
        profile.Title,
        profile.Headline,
        profile.About,
        profile.Location,
        profile.Email,
        profile.Phone,
        profile.PhotoUrl,
        profile.ResumeUrl,
        PeriodFormatter.YearsSince(profile.CareerStartYear, now),
        profile.SocialLinks
            .OrderBy(link => link.SortOrder)
            .Select(link => new SocialLinkDto(link.Name, link.Url, link.Icon))
            .ToList());

    public static SkillCategoryDto ToDto(this SkillCategory category) => new(
        category.Name,
        category.Icon,
        category.Skills
            .OrderBy(skill => skill.SortOrder)
            .Select(skill => skill.Name)
            .ToList());

    public static ProjectDto ToDto(this Project project) => new(
        project.Id,
        project.Title,
        project.Summary,
        project.Description,
        project.ImageUrl,
        project.DemoUrl,
        project.SourceUrl,
        project.Year,
        project.Technologies
            .OrderBy(technology => technology.SortOrder)
            .Select(technology => technology.Name)
            .ToList());

    public static ExperienceDto ToDto(this Experience experience) => new(
        experience.Company,
        experience.Position,
        PeriodFormatter.Format(experience.StartDate, experience.EndDate),
        experience.EndDate is null,
        SplitHighlights(experience.Description));

    /// <summary>Описание хранится строками — каждая непустая строка становится пунктом списка.</summary>
    private static IReadOnlyList<string> SplitHighlights(string description) =>
        description
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();
}
