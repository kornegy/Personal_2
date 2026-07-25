using Portfolio.Domain.Entities;
using Portfolio.Shared.Contracts;

namespace Portfolio.Infrastructure.Persistence.Seed;

/// <summary>
/// РУССКАЯ ВЕРСИЯ САЙТА — весь текст лежит здесь.
///
/// Данные взяты из резюме. Чтобы обновить сайт, правьте только этот файл
/// и его английскую пару <see cref="EnglishContent"/>: код трогать не нужно.
/// Код языка проставляется автоматически, указывать его в сущностях не надо.
/// </summary>
internal sealed class RussianContent : IContentPack
{
    public string LanguageCode => Languages.Russian;

    public Profile CreateProfile() => new()
    {
        FullName = "Артем Коваль",
        Title = "Front-End разработчик · React и TypeScript",
        Headline = "3 года коммерческого опыта: CRM-системы, сайты и веб-приложения на React и TypeScript.",
        About =
            "Front-End разработчик с трёхлетним коммерческим опытом. Делал CRM-системы, сайты " +
            "и веб-приложения на React и TypeScript.\n" +
            "В Bovios Pharm развивал внутренние CRM и веб-приложения компании: многостраничные интерфейсы " +
            "со сложным состоянием на MobX, адаптивные компоненты на Material-UI и Tailwind CSS, " +
            "интеграцию с REST API через Axios.\n" +
            "Постоянно осваиваю новые технологии и углубляю то, что уже умею. Бакалавр по компьютерным наукам " +
            "Хмельницкого национального университета. Украинский — родной, английский — B1.\n" +
            "Открыт к проектной работе и к позиции в команде.",
        Location = "Хмельницкий, Украина",
        Email = "krager.2108@gmail.com",
        Phone = "+38 098 940 55 18",
        PhotoUrl = "img/avatar.svg", // замените на своё фото: положите файл в wwwroot/img/
        ResumeUrl = "files/artem-koval-cv-en.pdf",
        YearsOfExperience = 3,
        SocialLinks =
        [
            new SocialLink
            {
                Name = "LinkedIn",
                Url = "https://linkedin.com/in/artem-koval-669921187",
                Icon = "linkedin",
                SortOrder = 1
            },
            new SocialLink
            {
                Name = "Email",
                Url = "mailto:krager.2108@gmail.com",
                Icon = "envelope",
                SortOrder = 2
            }
        ]
    };

    public IReadOnlyList<SkillCategory> CreateSkillCategories() =>
    [
        new SkillCategory
        {
            Name = "Основа",
            Icon = "code-slash",
            SortOrder = 1,
            Skills =
            [
                new Skill { Name = "React.js", SortOrder = 1 },
                new Skill { Name = "TypeScript", SortOrder = 2 },
                new Skill { Name = "JavaScript", SortOrder = 3 },
                new Skill { Name = "HTML / CSS", SortOrder = 4 }
            ]
        },
        new SkillCategory
        {
            Name = "Состояние и данные",
            Icon = "diagram-3",
            SortOrder = 2,
            Skills =
            [
                new Skill { Name = "MobX", SortOrder = 1 },
                new Skill { Name = "Axios", SortOrder = 2 },
                new Skill { Name = "REST API", SortOrder = 3 },
                new Skill { Name = "React Router", SortOrder = 4 }
            ]
        },
        new SkillCategory
        {
            Name = "Интерфейс",
            Icon = "palette",
            SortOrder = 3,
            Skills =
            [
                new Skill { Name = "Material-UI", SortOrder = 1 },
                new Skill { Name = "Tailwind CSS", SortOrder = 2 },
                new Skill { Name = "Адаптивная вёрстка", SortOrder = 3 }
            ]
        },
        new SkillCategory
        {
            Name = "Работа в команде",
            Icon = "tools",
            SortOrder = 4,
            Skills =
            [
                new Skill { Name = "Git", SortOrder = 1 },
                new Skill { Name = "Код-ревью", SortOrder = 2 },
                new Skill { Name = "Планирование задач", SortOrder = 3 }
            ]
        }
    ];

    // В резюме отдельного раздела с проектами нет, поэтому карточки собраны
    // из описания работы в Bovios Pharm. Замените их на реальные кейсы со ссылками,
    // когда сможете их показать, — это самая продающая часть сайта.
    public IReadOnlyList<Project> CreateProjects() =>
    [
        new Project
        {
            Title = "CRM для фармацевтической компании",
            Summary = "Внутренняя CRM на React и TypeScript, которую развивал почти три года.",
            Description =
                "Многостраничное приложение со сложным состоянием на MobX. Отвечал за разработку " +
                "и поддержку: новые разделы, доработку существующих и разбор ошибок.",
            ImageUrl = null,
            DemoUrl = null,
            SourceUrl = null,
            Year = null,
            SortOrder = 1,
            Technologies =
            [
                new ProjectTechnology { Name = "React", SortOrder = 1 },
                new ProjectTechnology { Name = "TypeScript", SortOrder = 2 },
                new ProjectTechnology { Name = "MobX", SortOrder = 3 },
                new ProjectTechnology { Name = "Material-UI", SortOrder = 4 }
            ]
        },
        new Project
        {
            Title = "Внутренние веб-приложения",
            Summary = "Набор рабочих инструментов для сотрудников компании.",
            Description =
                "Адаптивные интерфейсы на Material-UI и Tailwind CSS, интеграция с REST API " +
                "через Axios, маршрутизация на React Router.",
            ImageUrl = null,
            DemoUrl = null,
            SourceUrl = null,
            Year = null,
            SortOrder = 2,
            Technologies =
            [
                new ProjectTechnology { Name = "React", SortOrder = 1 },
                new ProjectTechnology { Name = "TypeScript", SortOrder = 2 },
                new ProjectTechnology { Name = "Tailwind CSS", SortOrder = 3 },
                new ProjectTechnology { Name = "Axios", SortOrder = 4 },
                new ProjectTechnology { Name = "React Router", SortOrder = 5 }
            ]
        }
    ];

    public IReadOnlyList<Experience> CreateExperiences() =>
    [
        new Experience
        {
            Company = "Bovios Pharm",
            Position = "React-разработчик",
            Description =
                "Разработка и поддержка CRM-систем и внутренних веб-приложений на React.js и TypeScript.\n" +
                "Управление состоянием через MobX в сложных многостраничных приложениях.\n" +
                "Адаптивные компоненты интерфейса на Material-UI и Tailwind CSS.\n" +
                "Интеграция с REST API через Axios, маршрутизация на React Router.\n" +
                "Работа в команде через Git: код-ревью и планирование задач.",
            StartDate = new DateOnly(2022, 9, 1),
            EndDate = new DateOnly(2025, 7, 1),
            SortOrder = 1
        }
    ];
}
