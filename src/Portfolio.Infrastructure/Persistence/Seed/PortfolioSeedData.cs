using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence.Seed;

/// <summary>
/// Собирает начальные данные из всех языковых пакетов и проставляет им код языка.
/// Сами тексты лежат в <see cref="UkrainianContent"/> и <see cref="EnglishContent"/>.
/// </summary>
internal static class PortfolioSeedData
{
    private static readonly IContentPack[] Packs = [new UkrainianContent(), new EnglishContent()];

    public static IReadOnlyList<Profile> CreateProfiles() =>
        Packs.Select(pack =>
        {
            var profile = pack.CreateProfile();
            profile.LanguageCode = pack.LanguageCode;
            return profile;
        }).ToList();

    public static IReadOnlyList<SkillCategory> CreateSkillCategories() =>
        Packs.SelectMany(pack => pack.CreateSkillCategories().Select(category =>
        {
            category.LanguageCode = pack.LanguageCode;
            return category;
        })).ToList();

    public static IReadOnlyList<Project> CreateProjects() =>
        Packs.SelectMany(pack => pack.CreateProjects().Select(project =>
        {
            project.LanguageCode = pack.LanguageCode;
            return project;
        })).ToList();

    public static IReadOnlyList<Experience> CreateExperiences() =>
        Packs.SelectMany(pack => pack.CreateExperiences().Select(experience =>
        {
            experience.LanguageCode = pack.LanguageCode;
            return experience;
        })).ToList();
}
