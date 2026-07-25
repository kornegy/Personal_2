using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence.Seed;

/// <summary>
/// Содержимое сайта на одном языке.
/// Чтобы добавить новый язык, достаточно написать ещё одну реализацию
/// и зарегистрировать её в <see cref="PortfolioSeedData"/>.
/// </summary>
internal interface IContentPack
{
    /// <summary>Код языка: «ru», «en». Проставляется сущностям автоматически.</summary>
    string LanguageCode { get; }

    Profile CreateProfile();

    IReadOnlyList<SkillCategory> CreateSkillCategories();

    IReadOnlyList<Project> CreateProjects();

    IReadOnlyList<Experience> CreateExperiences();
}
