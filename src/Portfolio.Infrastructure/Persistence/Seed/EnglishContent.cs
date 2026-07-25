using Portfolio.Domain.Entities;
using Portfolio.Shared.Contracts;

namespace Portfolio.Infrastructure.Persistence.Seed;

/// <summary>
/// ENGLISH VERSION OF THE SITE — all text lives here.
///
/// Taken from the CV. Keep it in sync with <see cref="UkrainianContent"/>:
/// same projects, same jobs, same order. The language code is applied automatically.
/// </summary>
internal sealed class EnglishContent : IContentPack
{
    public string LanguageCode => Languages.English;

    public Profile CreateProfile() => new()
    {
        FullName = "Artem Koval",
        Title = "Front-End Developer · React & TypeScript",
        Headline = "3 years of commercial experience building CRM systems, websites and web applications.",
        About =
            "Front-End Developer with 3 years of commercial experience building CRM systems, websites " +
            "and web applications in React and TypeScript.\n" +
            "At Bovios Pharm I developed and maintained the company's internal CRM and web applications: " +
            "multi-page interfaces with complex state in MobX, responsive components built with Material-UI " +
            "and Tailwind CSS, and REST API integration through Axios.\n" +
            "Constantly learning new technologies and improving existing skills. Bachelor's in Computer Science " +
            "from Khmelnytskyi National University. Ukrainian — native, English — B1.\n" +
            "Open to project work and to joining a team.",
        Location = "Khmelnytskyi, Ukraine",
        Email = "krager.2108@gmail.com",
        Phone = "+38 098 940 55 18",
        PhotoUrl = "img/avatar.svg", // replace with your own photo in wwwroot/img/
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
            Name = "Core",
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
            Name = "State and data",
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
            Name = "Interface",
            Icon = "palette",
            SortOrder = 3,
            Skills =
            [
                new Skill { Name = "Material-UI", SortOrder = 1 },
                new Skill { Name = "Tailwind CSS", SortOrder = 2 },
                new Skill { Name = "Responsive layouts", SortOrder = 3 }
            ]
        },
        new SkillCategory
        {
            Name = "Teamwork",
            Icon = "tools",
            SortOrder = 4,
            Skills =
            [
                new Skill { Name = "Git", SortOrder = 1 },
                new Skill { Name = "Code review", SortOrder = 2 },
                new Skill { Name = "Feature planning", SortOrder = 3 }
            ]
        }
    ];

    // The CV has no separate projects section, so these cards are written from the
    // Bovios Pharm job description. Replace them with real case studies and links
    // once you can show them — this is the most persuasive part of the site.
    public IReadOnlyList<Project> CreateProjects() =>
    [
        new Project
        {
            Title = "CRM for a pharmaceutical company",
            Summary = "Internal CRM in React and TypeScript, developed over almost three years.",
            Description =
                "A multi-page application with complex state managed by MobX. I was responsible for " +
                "development and maintenance: new sections, improvements to existing ones and bug fixing.",
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
            Title = "Internal web applications",
            Summary = "A set of working tools for the company's staff.",
            Description =
                "Responsive interfaces built with Material-UI and Tailwind CSS, REST API integration " +
                "through Axios and routing with React Router.",
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
            Position = "React Developer",
            Description =
                "Developed and maintained CRM systems and internal web applications using React.js and TypeScript.\n" +
                "Implemented state management with MobX across complex multi-page applications.\n" +
                "Built responsive UI components with Material-UI and Tailwind CSS.\n" +
                "Integrated REST APIs using Axios and managed routing with React Router.\n" +
                "Collaborated using Git; participated in code reviews and feature planning.",
            StartDate = new DateOnly(2022, 9, 1),
            EndDate = new DateOnly(2025, 7, 1),
            SortOrder = 1
        }
    ];
}
