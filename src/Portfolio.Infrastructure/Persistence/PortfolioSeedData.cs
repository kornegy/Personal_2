using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence;

/// <summary>
/// ЕДИНСТВЕННОЕ МЕСТО, ГДЕ ЛЕЖИТ КОНТЕНТ САЙТА.
///
/// Чтобы обновить сайт под своё резюме, правьте только этот файл: код трогать не нужно.
/// После изменений удалите файл базы (portfolio.db) и перезапустите приложение —
/// база пересоздастся с новыми данными.
/// </summary>
internal static class PortfolioSeedData
{
    public static Profile CreateProfile() => new()
    {
        FullName = "Назар", // укажите имя и фамилию как в резюме
        Title = "Front-End React разработчик",
        Headline = "Собираю быстрые и аккуратные интерфейсы на React и TypeScript — от макета до продакшена.",
        About =
            "Front-End разработчик с фокусом на React-экосистеме. Беру задачу от макета и довожу до " +
            "рабочего продукта: продумываю структуру компонентов, состояние, интеграцию с API и поведение " +
            "интерфейса на всех размерах экрана.\n" +
            "В работе важна предсказуемость: понятный код, который спокойно передаётся другому разработчику, " +
            "адаптивная вёрстка без костылей и внимание к скорости загрузки.\n" +
            "Открыт к проектной работе и к позиции в команде. Отвечаю на письма в течение рабочего дня.",
        Location = "Удалённо",
        Email = "nazark.2810@gmail.com",
        Phone = null,
        PhotoUrl = "img/avatar.svg",
        ResumeUrl = null, // положите PDF в wwwroot/files/cv.pdf и укажите "files/cv.pdf"
        CareerStartYear = 2022,
        SocialLinks =
        [
            new SocialLink { Name = "GitHub", Url = "https://github.com/kornegy", Icon = "github", SortOrder = 1 },
            new SocialLink { Name = "Telegram", Url = "https://t.me/", Icon = "telegram", SortOrder = 2 },
            new SocialLink { Name = "Email", Url = "mailto:nazark.2810@gmail.com", Icon = "envelope", SortOrder = 3 }
        ]
    };

    public static IReadOnlyList<SkillCategory> CreateSkillCategories() =>
    [
        new SkillCategory
        {
            Name = "Основа",
            Icon = "code-slash",
            SortOrder = 1,
            Skills =
            [
                new Skill { Name = "JavaScript (ES6+)", SortOrder = 1 },
                new Skill { Name = "TypeScript", SortOrder = 2 },
                new Skill { Name = "HTML5", SortOrder = 3 },
                new Skill { Name = "CSS3", SortOrder = 4 }
            ]
        },
        new SkillCategory
        {
            Name = "React-экосистема",
            Icon = "boxes",
            SortOrder = 2,
            Skills =
            [
                new Skill { Name = "React", SortOrder = 1 },
                new Skill { Name = "React Router", SortOrder = 2 },
                new Skill { Name = "Redux Toolkit", SortOrder = 3 },
                new Skill { Name = "React Query", SortOrder = 4 },
                new Skill { Name = "Next.js", SortOrder = 5 }
            ]
        },
        new SkillCategory
        {
            Name = "Вёрстка и стили",
            Icon = "palette",
            SortOrder = 3,
            Skills =
            [
                new Skill { Name = "Адаптивная вёрстка", SortOrder = 1 },
                new Skill { Name = "Sass / SCSS", SortOrder = 2 },
                new Skill { Name = "Tailwind CSS", SortOrder = 3 },
                new Skill { Name = "Bootstrap", SortOrder = 4 },
                new Skill { Name = "Figma", SortOrder = 5 }
            ]
        },
        new SkillCategory
        {
            Name = "Инструменты",
            Icon = "tools",
            SortOrder = 4,
            Skills =
            [
                new Skill { Name = "Git / GitHub", SortOrder = 1 },
                new Skill { Name = "Vite", SortOrder = 2 },
                new Skill { Name = "Webpack", SortOrder = 3 },
                new Skill { Name = "REST API", SortOrder = 4 },
                new Skill { Name = "Jest / Testing Library", SortOrder = 5 }
            ]
        }
    ];

    public static IReadOnlyList<Project> CreateProjects() =>
    [
        new Project
        {
            Title = "Интернет-магазин",
            Summary = "SPA-витрина с корзиной, фильтрами и оформлением заказа.",
            Description =
                "Каталог с фильтрацией и поиском, корзина с сохранением состояния между сессиями, " +
                "пошаговое оформление заказа и интеграция с REST API. Списки товаров подгружаются " +
                "постранично, тяжёлые страницы разделены на отдельные бандлы.",
            ImageUrl = null,
            DemoUrl = null,
            SourceUrl = null,
            Year = 2024,
            SortOrder = 1,
            Technologies =
            [
                new ProjectTechnology { Name = "React", SortOrder = 1 },
                new ProjectTechnology { Name = "TypeScript", SortOrder = 2 },
                new ProjectTechnology { Name = "Redux Toolkit", SortOrder = 3 },
                new ProjectTechnology { Name = "SCSS", SortOrder = 4 }
            ]
        },
        new Project
        {
            Title = "Панель аналитики",
            Summary = "Дашборд с графиками и фильтрами по датам для внутренней команды.",
            Description =
                "Интерфейс для работы с большими таблицами: сортировка, фильтры, экспорт. " +
                "Данные кэшируются на клиенте, поэтому переключение между разделами происходит без ожидания.",
            ImageUrl = null,
            DemoUrl = null,
            SourceUrl = null,
            Year = 2024,
            SortOrder = 2,
            Technologies =
            [
                new ProjectTechnology { Name = "React", SortOrder = 1 },
                new ProjectTechnology { Name = "React Query", SortOrder = 2 },
                new ProjectTechnology { Name = "Chart.js", SortOrder = 3 },
                new ProjectTechnology { Name = "Tailwind CSS", SortOrder = 4 }
            ]
        },
        new Project
        {
            Title = "Лендинг для студии",
            Summary = "Промо-страница с анимациями и формой заявки.",
            Description =
                "Вёрстка по макету Figma с точностью до пикселя, плавные анимации при скролле, " +
                "форма заявки с валидацией. Оценка Lighthouse — 95+ по всем категориям.",
            ImageUrl = null,
            DemoUrl = null,
            SourceUrl = null,
            Year = 2023,
            SortOrder = 3,
            Technologies =
            [
                new ProjectTechnology { Name = "Next.js", SortOrder = 1 },
                new ProjectTechnology { Name = "TypeScript", SortOrder = 2 },
                new ProjectTechnology { Name = "Framer Motion", SortOrder = 3 }
            ]
        }
    ];

    public static IReadOnlyList<Experience> CreateExperiences() =>
    [
        new Experience
        {
            Company = "Фриланс",
            Position = "Front-End React разработчик",
            Description =
                "Разработка SPA и лендингов под ключ: от разбора макета до сборки и деплоя.\n" +
                "Интеграция интерфейсов с REST API, обработка ошибок и состояний загрузки.\n" +
                "Адаптивная вёрстка и кроссбраузерная проверка на реальных устройствах.",
            StartDate = new DateOnly(2023, 6, 1),
            EndDate = null,
            SortOrder = 1
        },
        new Experience
        {
            Company = "Продуктовая команда",
            Position = "Front-End разработчик",
            Description =
                "Развитие клиентской части веб-приложения на React и TypeScript.\n" +
                "Перевод legacy-компонентов на функциональный подход с хуками.\n" +
                "Ускорение первой загрузки за счёт code splitting и оптимизации изображений.",
            StartDate = new DateOnly(2022, 3, 1),
            EndDate = new DateOnly(2023, 5, 1),
            SortOrder = 2
        }
    ];
}
