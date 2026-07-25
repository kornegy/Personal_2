using Portfolio.Shared.Contracts;

namespace Portfolio.Client.Localization;

/// <summary>Украинские подписи интерфейса.</summary>
internal static class UkrainianStrings
{
    public static readonly UiStrings Value = new()
    {
        BrandText = "Портфоліо",
        NavAriaLabel = "Розділи сайту",
        NavAbout = "Про мене",
        NavSkills = "Технології",
        NavProjects = "Проєкти",
        NavExperience = "Досвід",
        NavContactCta = "Написати мені",
        MenuOpen = "Відкрити меню",
        MenuClose = "Закрити меню",
        LanguageSwitchLabel = "Мова сайту",
        FooterText = "портфоліо Front-End розробника",
        FooterToTop = "Нагору",

        HeroBadge = "Відкритий до нових проєктів",
        HeroCtaContact = "Обговорити проєкт",
        HeroCtaProjects = "Переглянути роботи",
        HeroCtaResume = "Резюме",
        HeroExperienceSuffix = "комерційного досвіду",
        HeroPhotoAlt = "Фотографія",
        YearsWord = PluralizeYears,

        AboutLabel = "Про мене",
        AboutTitle = "Хто я і чим корисний",
        AboutStatYears = "років у розробці",
        AboutStatProjects = "проєктів у портфоліо",
        AboutStatTechnologies = "технологій у роботі",

        SkillsLabel = "Технології",
        SkillsTitle = "З чим я працюю",
        SkillsSubtitle = "Інструменти, які використовую щодня і готовий застосувати на вашому проєкті.",
        SkillsUnavailable = "Список технологій недоступний",

        ProjectsLabel = "Проєкти",
        ProjectsTitle = "Вибрані роботи",
        ProjectsSubtitle = "Кілька задач, за якими видно підхід до роботи та рівень опрацювання деталей.",
        ProjectsUnavailable = "Проєкти недоступні",
        ProjectLinkDemo = "Демо",
        ProjectLinkSource = "Код",
        ProjectPreviewAlt = "Прев'ю проєкту",
        ProjectTechCountLabel = "технологій",

        ExperienceLabel = "Досвід",
        ExperienceTitle = "Де і над чим працював",
        ExperienceUnavailable = "Дані про досвід недоступні",

        ContactLabel = "Контакти",
        ContactTitle = "Обговорімо завдання",
        ContactStartProject = "Розпочати проєкт",
        ContactPhoneLabel = "Телефон",
        ContactLocationLabel = "Локація",
        ContactRights = "Усі права захищені",
        ContactSubtitle = "Напишіть кілька слів про проєкт — відповім протягом робочого дня.",
        ContactFieldName = "Ім'я",
        ContactFieldEmail = "Email",
        ContactFieldSubject = "Тема",
        ContactFieldMessage = "Повідомлення",
        ContactPlaceholderName = "Як до вас звертатися",
        ContactPlaceholderEmail = "you@example.com",
        ContactPlaceholderSubject = "Наприклад: лендінг для студії",
        ContactPlaceholderMessage = "Опишіть завдання, терміни та бюджет",
        ContactHoneypotLabel = "Не заповнюйте це поле",
        ContactSubmit = "Надіслати повідомлення",
        ContactSending = "Надсилаємо…",

        Loading = "Завантажуємо дані…",
        LoadingPortfolio = "Завантажуємо портфоліо…",
        UnavailableTitle = "Сайт тимчасово недоступний",
        UnavailableText = "Не вдалося завантажити дані. Оновіть сторінку за хвилину.",
        NotFoundTitle = "Сторінку не знайдено",
        NotFoundText = "Такої сторінки немає — поверніться на головну.",
        NotFoundLink = "На головну",
        PageTitleFallback = "Портфоліо",
        PageTitleRoleFallback = "Front-End розробник",
        MetaDescription = "Front-End розробник на React і TypeScript. Адаптивні інтерфейси, інтеграція з API, чистий і підтримуваний код.",

        Validation = new Dictionary<string, string>
        {
            [ValidationKeys.NameRequired] = "Вкажіть ім'я",
            [ValidationKeys.NameLength] = "Ім'я має бути від 2 до 80 символів",
            [ValidationKeys.EmailRequired] = "Вкажіть email",
            [ValidationKeys.EmailInvalid] = "Некоректний email",
            [ValidationKeys.EmailLength] = "Email задовгий",
            [ValidationKeys.SubjectRequired] = "Вкажіть тему",
            [ValidationKeys.SubjectLength] = "Тема має бути від 3 до 120 символів",
            [ValidationKeys.MessageRequired] = "Напишіть повідомлення",
            [ValidationKeys.MessageLength] = "Повідомлення має бути від 10 до 2000 символів"
        },

        ContactStatus = new Dictionary<string, string>
        {
            [ContactResultCodes.Accepted] = "Повідомлення надіслано. Відповім протягом робочого дня.",
            [ContactResultCodes.RateLimited] = "Забагато повідомлень поспіль. Спробуйте пізніше або напишіть на пошту.",
            [ContactResultCodes.Failed] = "Не вдалося надіслати повідомлення. Спробуйте ще раз.",
            [ContactResultCodes.NetworkError] = "Немає зв'язку з сервером. Перевірте підключення та спробуйте знову."
        }
    };

    /// <summary>«1 рік», «3 роки», «5 років».</summary>
    private static string PluralizeYears(int value)
    {
        var lastTwo = value % 100;
        if (lastTwo is >= 11 and <= 14)
        {
            return "років";
        }

        return (value % 10) switch
        {
            1 => "рік",
            2 or 3 or 4 => "роки",
            _ => "років"
        };
    }
}
