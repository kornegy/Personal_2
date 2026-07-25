namespace Portfolio.Client.Localization;

/// <summary>
/// Все подписи интерфейса на одном языке.
///
/// Вместо .resx-файлов используем обычный класс: строк немного, зато компилятор
/// проверяет, что при добавлении подписи её перевели на все языки — иначе сборка упадёт.
/// </summary>
public sealed class UiStrings
{
    // Шапка и подвал
    public required string BrandText { get; init; }
    public required string NavAriaLabel { get; init; }
    public required string NavAbout { get; init; }
    public required string NavSkills { get; init; }
    public required string NavProjects { get; init; }
    public required string NavExperience { get; init; }
    public required string NavContactCta { get; init; }
    public required string MenuOpen { get; init; }
    public required string MenuClose { get; init; }
    public required string LanguageSwitchLabel { get; init; }
    public required string FooterText { get; init; }
    public required string FooterToTop { get; init; }

    // Первый экран
    public required string HeroBadge { get; init; }
    public required string HeroCtaContact { get; init; }
    public required string HeroCtaProjects { get; init; }
    public required string HeroCtaResume { get; init; }
    public required string HeroExperienceSuffix { get; init; }
    public required string HeroPhotoAlt { get; init; }

    /// <summary>«3 года» / «3 years» — правила склонения у языков разные.</summary>
    public required Func<int, string> YearsWord { get; init; }

    // Обо мне
    public required string AboutLabel { get; init; }
    public required string AboutTitle { get; init; }
    public required string AboutStatYears { get; init; }
    public required string AboutStatProjects { get; init; }
    public required string AboutStatTechnologies { get; init; }

    // Технологии
    public required string SkillsLabel { get; init; }
    public required string SkillsTitle { get; init; }
    public required string SkillsSubtitle { get; init; }
    public required string SkillsUnavailable { get; init; }

    // Проекты
    public required string ProjectsLabel { get; init; }
    public required string ProjectsTitle { get; init; }
    public required string ProjectsSubtitle { get; init; }
    public required string ProjectsUnavailable { get; init; }
    public required string ProjectLinkDemo { get; init; }
    public required string ProjectLinkSource { get; init; }
    public required string ProjectPreviewAlt { get; init; }
    public required string ProjectTechCountLabel { get; init; }

    // Опыт
    public required string ExperienceLabel { get; init; }
    public required string ExperienceTitle { get; init; }
    public required string ExperienceUnavailable { get; init; }

    // Контакты
    public required string ContactLabel { get; init; }
    public required string ContactTitle { get; init; }

    /// <summary>Крупная надпись в финальной секции.</summary>
    public required string ContactStartProject { get; init; }
    public required string ContactPhoneLabel { get; init; }
    public required string ContactLocationLabel { get; init; }
    public required string ContactRights { get; init; }

    public required string ContactSubtitle { get; init; }
    public required string ContactFieldName { get; init; }
    public required string ContactFieldEmail { get; init; }
    public required string ContactFieldSubject { get; init; }
    public required string ContactFieldMessage { get; init; }
    public required string ContactPlaceholderName { get; init; }
    public required string ContactPlaceholderEmail { get; init; }
    public required string ContactPlaceholderSubject { get; init; }
    public required string ContactPlaceholderMessage { get; init; }
    public required string ContactHoneypotLabel { get; init; }
    public required string ContactSubmit { get; init; }
    public required string ContactSending { get; init; }

    // Общее
    public required string Loading { get; init; }
    public required string LoadingPortfolio { get; init; }
    public required string UnavailableTitle { get; init; }
    public required string UnavailableText { get; init; }
    public required string NotFoundTitle { get; init; }
    public required string NotFoundText { get; init; }
    public required string NotFoundLink { get; init; }
    public required string PageTitleFallback { get; init; }
    public required string PageTitleRoleFallback { get; init; }
    public required string MetaDescription { get; init; }

    /// <summary>Тексты ошибок валидации по ключам из <c>ValidationKeys</c>.</summary>
    public required IReadOnlyDictionary<string, string> Validation { get; init; }

    /// <summary>Тексты ответов формы по кодам из <c>ContactResultCodes</c>.</summary>
    public required IReadOnlyDictionary<string, string> ContactStatus { get; init; }
}
