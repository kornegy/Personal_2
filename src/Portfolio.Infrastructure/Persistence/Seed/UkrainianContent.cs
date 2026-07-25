using Portfolio.Domain.Entities;
using Portfolio.Shared.Contracts;

namespace Portfolio.Infrastructure.Persistence.Seed;

/// <summary>
/// УКРАИНСКАЯ ВЕРСИЯ САЙТА — весь текст лежит здесь.
///
/// Данные взяты из резюме. Чтобы обновить сайт, правьте только этот файл
/// и его английскую пару <see cref="EnglishContent"/>: код трогать не нужно.
/// Код языка проставляется автоматически, указывать его в сущностях не надо.
/// </summary>
internal sealed class UkrainianContent : IContentPack
{
    public string LanguageCode => Languages.Ukrainian;

    public Profile CreateProfile() => new()
    {
        FullName = "Артем Коваль",
        Title = "Front-End розробник · React і TypeScript",
        Headline = "3 роки комерційного досвіду: CRM-системи, сайти та вебзастосунки на React і TypeScript.",
        About =
            "Front-End розробник із трирічним комерційним досвідом. Робив CRM-системи, сайти " +
            "та вебзастосунки на React і TypeScript.\n" +
            "У Bovios Pharm розвивав внутрішні CRM і вебзастосунки компанії: багатосторінкові інтерфейси " +
            "зі складним станом на MobX, адаптивні компоненти на Material-UI і Tailwind CSS, " +
            "інтеграцію з REST API через Axios.\n" +
            "Постійно опановую нові технології та поглиблюю те, що вже вмію. Бакалавр з комп'ютерних наук " +
            "Хмельницького національного університету. Українська — рідна, англійська — B1.\n" +
            "Відкритий до проєктної роботи та до позиції в команді.",
        Location = "Хмельницький, Україна",
        Email = "krager.2108@gmail.com",
        Phone = "+38 098 940 55 18",
        PhotoUrl = "img/avatar.svg", // замініть на своє фото: покладіть файл у wwwroot/img/
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
            Name = "Стан і дані",
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
            Name = "Інтерфейс",
            Icon = "palette",
            SortOrder = 3,
            Skills =
            [
                new Skill { Name = "Material-UI", SortOrder = 1 },
                new Skill { Name = "Tailwind CSS", SortOrder = 2 },
                new Skill { Name = "Адаптивна верстка", SortOrder = 3 }
            ]
        },
        new SkillCategory
        {
            Name = "Робота в команді",
            Icon = "tools",
            SortOrder = 4,
            Skills =
            [
                new Skill { Name = "Git", SortOrder = 1 },
                new Skill { Name = "Код-рев'ю", SortOrder = 2 },
                new Skill { Name = "Планування задач", SortOrder = 3 }
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
            Title = "CRM для фармацевтичної компанії",
            Summary = "Внутрішня CRM на React і TypeScript, яку розвивав майже три роки.",
            Description =
                "Багатосторінковий застосунок зі складним станом на MobX. Відповідав за розробку " +
                "та підтримку: нові розділи, доопрацювання наявних і розбір помилок.",
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
            Title = "Внутрішні вебзастосунки",
            Summary = "Набір робочих інструментів для співробітників компанії.",
            Description =
                "Адаптивні інтерфейси на Material-UI і Tailwind CSS, інтеграція з REST API " +
                "через Axios, маршрутизація на React Router.",
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
            Position = "React-розробник",
            Description =
                "Розробка та підтримка CRM-систем і внутрішніх вебзастосунків на React.js і TypeScript.\n" +
                "Керування станом через MobX у складних багатосторінкових застосунках.\n" +
                "Адаптивні компоненти інтерфейсу на Material-UI і Tailwind CSS.\n" +
                "Інтеграція з REST API через Axios, маршрутизація на React Router.\n" +
                "Робота в команді через Git: код-рев'ю та планування задач.",
            StartDate = new DateOnly(2022, 9, 1),
            EndDate = new DateOnly(2025, 7, 1),
            SortOrder = 1
        }
    ];
}
