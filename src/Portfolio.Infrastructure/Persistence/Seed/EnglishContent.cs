using Portfolio.Domain.Entities;
using Portfolio.Shared.Contracts;

namespace Portfolio.Infrastructure.Persistence.Seed;

/// <summary>
/// ENGLISH VERSION OF THE SITE — all text lives here.
///
/// Keep it in sync with <see cref="RussianContent"/>: same projects, same jobs, same order.
/// The language code is applied automatically, no need to set it on entities.
/// </summary>
internal sealed class EnglishContent : IContentPack
{
    public string LanguageCode => Languages.English;

    public Profile CreateProfile() => new()
    {
        FullName = "Nazar", // put your full name here, same as in the CV
        Title = "Front-End React Developer",
        Headline = "I build fast, well-crafted interfaces with React and TypeScript — from mockup to production.",
        About =
            "Front-End developer focused on the React ecosystem. I take a task from the design file all the way " +
            "to a working product: component structure, state management, API integration and how the interface " +
            "behaves on every screen size.\n" +
            "Predictability matters to me: readable code another developer can pick up without a handover call, " +
            "responsive layouts without hacks, and attention to loading speed.\n" +
            "Open to project work and to joining a team. I reply to emails within one business day.",
        Location = "Remote",
        Email = "nazark.2810@gmail.com",
        Phone = null,
        PhotoUrl = "img/avatar.svg",
        ResumeUrl = null, // drop the PDF into wwwroot/files/cv-en.pdf and set "files/cv-en.pdf"
        CareerStartYear = 2022,
        SocialLinks =
        [
            new SocialLink { Name = "GitHub", Url = "https://github.com/kornegy", Icon = "github", SortOrder = 1 },
            new SocialLink { Name = "Telegram", Url = "https://t.me/", Icon = "telegram", SortOrder = 2 },
            new SocialLink { Name = "Email", Url = "mailto:nazark.2810@gmail.com", Icon = "envelope", SortOrder = 3 }
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
                new Skill { Name = "JavaScript (ES6+)", SortOrder = 1 },
                new Skill { Name = "TypeScript", SortOrder = 2 },
                new Skill { Name = "HTML5", SortOrder = 3 },
                new Skill { Name = "CSS3", SortOrder = 4 }
            ]
        },
        new SkillCategory
        {
            Name = "React ecosystem",
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
            Name = "Markup and styling",
            Icon = "palette",
            SortOrder = 3,
            Skills =
            [
                new Skill { Name = "Responsive layouts", SortOrder = 1 },
                new Skill { Name = "Sass / SCSS", SortOrder = 2 },
                new Skill { Name = "Tailwind CSS", SortOrder = 3 },
                new Skill { Name = "Bootstrap", SortOrder = 4 },
                new Skill { Name = "Figma", SortOrder = 5 }
            ]
        },
        new SkillCategory
        {
            Name = "Tooling",
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

    public IReadOnlyList<Project> CreateProjects() =>
    [
        new Project
        {
            Title = "E-commerce storefront",
            Summary = "Single-page storefront with cart, filters and checkout.",
            Description =
                "Catalogue with filtering and search, a cart that survives page reloads, a step-by-step " +
                "checkout and REST API integration. Product lists load page by page, and heavy routes " +
                "are split into separate bundles.",
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
            Title = "Analytics dashboard",
            Summary = "Internal dashboard with charts and date range filters.",
            Description =
                "An interface built around large tables: sorting, filtering and export. " +
                "Responses are cached on the client, so switching between sections feels instant.",
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
            Title = "Studio landing page",
            Summary = "Promo page with scroll animations and a lead form.",
            Description =
                "Pixel-accurate implementation of a Figma design, smooth scroll animations and a validated " +
                "lead form. Lighthouse scores 95+ across all categories.",
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

    public IReadOnlyList<Experience> CreateExperiences() =>
    [
        new Experience
        {
            Company = "Freelance",
            Position = "Front-End React Developer",
            Description =
                "End-to-end delivery of single-page apps and landing pages, from design review to deployment.\n" +
                "REST API integration, including error and loading states.\n" +
                "Responsive layouts verified across browsers on real devices.",
            StartDate = new DateOnly(2023, 6, 1),
            EndDate = null,
            SortOrder = 1
        },
        new Experience
        {
            Company = "Product team",
            Position = "Front-End Developer",
            Description =
                "Developed the client side of a web application in React and TypeScript.\n" +
                "Migrated legacy components to a functional approach with hooks.\n" +
                "Cut first-load time through code splitting and image optimisation.",
            StartDate = new DateOnly(2022, 3, 1),
            EndDate = new DateOnly(2023, 5, 1),
            SortOrder = 2
        }
    ];
}
