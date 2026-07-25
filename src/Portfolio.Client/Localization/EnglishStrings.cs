using Portfolio.Shared.Contracts;

namespace Portfolio.Client.Localization;

/// <summary>English UI labels.</summary>
internal static class EnglishStrings
{
    public static readonly UiStrings Value = new()
    {
        BrandText = "Portfolio",
        NavAriaLabel = "Site sections",
        NavAbout = "About",
        NavSkills = "Stack",
        NavProjects = "Projects",
        NavExperience = "Experience",
        NavContactCta = "Get in touch",
        MenuOpen = "Open menu",
        MenuClose = "Close menu",
        LanguageSwitchLabel = "Site language",
        FooterText = "Front-End developer portfolio",
        FooterToTop = "Back to top",

        HeroBadge = "Available for new projects",
        HeroCtaContact = "Discuss a project",
        HeroCtaProjects = "See my work",
        HeroCtaResume = "Résumé",
        HeroExperienceSuffix = "of commercial experience",
        HeroPhotoAlt = "Photo",
        YearsWord = value => value == 1 ? "year" : "years",

        AboutLabel = "About",
        AboutTitle = "Who I am and how I help",
        AboutStatYears = "years in development",
        AboutStatProjects = "projects in the portfolio",
        AboutStatTechnologies = "technologies in daily use",

        SkillsLabel = "Stack",
        SkillsTitle = "What I work with",
        SkillsSubtitle = "The tools I use every day and am ready to bring to your project.",
        SkillsUnavailable = "The tech list is unavailable",

        ProjectsLabel = "Projects",
        ProjectsTitle = "Selected work",
        ProjectsSubtitle = "A few tasks that show how I work and how far I take the details.",
        ProjectsUnavailable = "Projects are unavailable",
        ProjectLinkDemo = "Live demo",
        ProjectLinkSource = "Source",
        ProjectPreviewAlt = "Project preview",

        ExperienceLabel = "Experience",
        ExperienceTitle = "Where and what I worked on",
        ExperienceUnavailable = "Experience data is unavailable",

        ContactLabel = "Contact",
        ContactTitle = "Let's talk about your project",
        ContactSubtitle = "Tell me a couple of words about it — I reply within one business day.",
        ContactFieldName = "Name",
        ContactFieldEmail = "Email",
        ContactFieldSubject = "Subject",
        ContactFieldMessage = "Message",
        ContactPlaceholderName = "How should I address you",
        ContactPlaceholderSubject = "For example: landing page for a studio",
        ContactPlaceholderMessage = "Describe the task, timeline and budget",
        ContactHoneypotLabel = "Leave this field empty",
        ContactSubmit = "Send message",
        ContactSending = "Sending…",

        Loading = "Loading…",
        LoadingPortfolio = "Loading the portfolio…",
        UnavailableTitle = "The site is temporarily unavailable",
        UnavailableText = "The data could not be loaded. Please refresh the page in a minute.",
        NotFoundTitle = "Page not found",
        NotFoundText = "There is no such page — head back to the home page.",
        NotFoundLink = "Go home",
        PageTitleFallback = "Portfolio",
        PageTitleRoleFallback = "Front-End Developer",
        MetaDescription = "Front-End developer working with React and TypeScript. Responsive interfaces, API integration, clean and maintainable code.",

        Validation = new Dictionary<string, string>
        {
            [ValidationKeys.NameRequired] = "Please enter your name",
            [ValidationKeys.NameLength] = "The name must be 2 to 80 characters long",
            [ValidationKeys.EmailRequired] = "Please enter your email",
            [ValidationKeys.EmailInvalid] = "This email address is not valid",
            [ValidationKeys.EmailLength] = "The email address is too long",
            [ValidationKeys.SubjectRequired] = "Please enter a subject",
            [ValidationKeys.SubjectLength] = "The subject must be 3 to 120 characters long",
            [ValidationKeys.MessageRequired] = "Please write a message",
            [ValidationKeys.MessageLength] = "The message must be 10 to 2000 characters long"
        },

        ContactStatus = new Dictionary<string, string>
        {
            [ContactResultCodes.Accepted] = "Message sent. I will reply within one business day.",
            [ContactResultCodes.RateLimited] = "Too many messages in a row. Try again later or send me an email.",
            [ContactResultCodes.Failed] = "The message could not be sent. Please try again.",
            [ContactResultCodes.NetworkError] = "No connection to the server. Check your network and try again."
        }
    };
}
