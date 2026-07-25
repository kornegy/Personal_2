using Portfolio.Shared.Contracts;

namespace Portfolio.Client.Localization;

/// <summary>Русские подписи интерфейса.</summary>
internal static class RussianStrings
{
    public static readonly UiStrings Value = new()
    {
        BrandText = "Портфолио",
        NavAriaLabel = "Разделы сайта",
        NavAbout = "Обо мне",
        NavSkills = "Технологии",
        NavProjects = "Проекты",
        NavExperience = "Опыт",
        NavContactCta = "Написать мне",
        MenuOpen = "Открыть меню",
        MenuClose = "Закрыть меню",
        LanguageSwitchLabel = "Язык сайта",
        FooterText = "портфолио Front-End разработчика",
        FooterToTop = "Наверх",

        HeroBadge = "Открыт к новым проектам",
        HeroCtaContact = "Обсудить проект",
        HeroCtaProjects = "Посмотреть работы",
        HeroCtaResume = "Резюме",
        HeroExperienceSuffix = "коммерческого опыта",
        HeroPhotoAlt = "Фотография",
        YearsWord = PluralizeYears,

        AboutLabel = "Обо мне",
        AboutTitle = "Кто я и чем полезен",
        AboutStatYears = "лет в разработке",
        AboutStatProjects = "проектов в портфолио",
        AboutStatTechnologies = "технологий в работе",

        SkillsLabel = "Технологии",
        SkillsTitle = "С чем я работаю",
        SkillsSubtitle = "Инструменты, которые использую каждый день и готов применить на вашем проекте.",
        SkillsUnavailable = "Список технологий недоступен",

        ProjectsLabel = "Проекты",
        ProjectsTitle = "Избранные работы",
        ProjectsSubtitle = "Несколько задач, по которым видно подход к работе и уровень проработки деталей.",
        ProjectsUnavailable = "Проекты недоступны",
        ProjectLinkDemo = "Демо",
        ProjectLinkSource = "Код",
        ProjectPreviewAlt = "Превью проекта",

        ExperienceLabel = "Опыт",
        ExperienceTitle = "Где и над чем работал",
        ExperienceUnavailable = "Данные об опыте недоступны",

        ContactLabel = "Контакты",
        ContactTitle = "Давайте обсудим задачу",
        ContactSubtitle = "Напишите пару слов о проекте — отвечу в течение рабочего дня.",
        ContactFieldName = "Имя",
        ContactFieldEmail = "Email",
        ContactFieldSubject = "Тема",
        ContactFieldMessage = "Сообщение",
        ContactPlaceholderName = "Как к вам обращаться",
        ContactPlaceholderEmail = "you@example.com",
        ContactPlaceholderSubject = "Например: лендинг для студии",
        ContactPlaceholderMessage = "Опишите задачу, сроки и бюджет",
        ContactHoneypotLabel = "Не заполняйте это поле",
        ContactSubmit = "Отправить сообщение",
        ContactSending = "Отправляем…",

        Loading = "Загружаем данные…",
        LoadingPortfolio = "Загружаем портфолио…",
        UnavailableTitle = "Сайт временно недоступен",
        UnavailableText = "Не удалось загрузить данные. Обновите страницу через минуту.",
        NotFoundTitle = "Страница не найдена",
        NotFoundText = "Такой страницы нет — вернитесь на главную.",
        NotFoundLink = "На главную",
        PageTitleFallback = "Портфолио",
        PageTitleRoleFallback = "Front-End разработчик",
        MetaDescription = "Front-End разработчик на React и TypeScript. Адаптивные интерфейсы, интеграция с API, чистый и поддерживаемый код.",

        Validation = new Dictionary<string, string>
        {
            [ValidationKeys.NameRequired] = "Укажите имя",
            [ValidationKeys.NameLength] = "Имя должно быть от 2 до 80 символов",
            [ValidationKeys.EmailRequired] = "Укажите email",
            [ValidationKeys.EmailInvalid] = "Некорректный email",
            [ValidationKeys.EmailLength] = "Email слишком длинный",
            [ValidationKeys.SubjectRequired] = "Укажите тему",
            [ValidationKeys.SubjectLength] = "Тема должна быть от 3 до 120 символов",
            [ValidationKeys.MessageRequired] = "Напишите сообщение",
            [ValidationKeys.MessageLength] = "Сообщение должно быть от 10 до 2000 символов"
        },

        ContactStatus = new Dictionary<string, string>
        {
            [ContactResultCodes.Accepted] = "Сообщение отправлено. Отвечу в течение рабочего дня.",
            [ContactResultCodes.RateLimited] = "Слишком много сообщений подряд. Попробуйте позже или напишите на почту.",
            [ContactResultCodes.Failed] = "Не получилось отправить сообщение. Попробуйте ещё раз.",
            [ContactResultCodes.NetworkError] = "Нет связи с сервером. Проверьте подключение и попробуйте снова."
        }
    };

    /// <summary>«1 год», «3 года», «5 лет».</summary>
    private static string PluralizeYears(int value)
    {
        var lastTwo = value % 100;
        if (lastTwo is >= 11 and <= 14)
        {
            return "лет";
        }

        return (value % 10) switch
        {
            1 => "год",
            2 or 3 or 4 => "года",
            _ => "лет"
        };
    }
}
