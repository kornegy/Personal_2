using Portfolio.Application.Abstractions;

namespace Portfolio.Api.Endpoints;

/// <summary>
/// Только чтение: содержимое сайта. Язык передаётся параметром ?lang=ru|en,
/// неизвестное значение молча заменяется на язык по умолчанию.
/// Ответы кэшируются отдельно по каждому языку.
/// </summary>
public static class PortfolioEndpoints
{
    public const string CachePolicy = "portfolio-content";

    public const string LanguageQueryParameter = "lang";

    public static IEndpointRouteBuilder MapPortfolioEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api").CacheOutput(CachePolicy);

        group.MapGet("/profile", async (string? lang, IPortfolioService service, CancellationToken cancellationToken) =>
        {
            var profile = await service.GetProfileAsync(lang ?? string.Empty, cancellationToken);
            return profile is null ? Results.NotFound() : Results.Ok(profile);
        })
        .WithName("GetProfile");

        group.MapGet("/skills", async (string? lang, IPortfolioService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GetSkillsAsync(lang ?? string.Empty, cancellationToken)))
            .WithName("GetSkills");

        group.MapGet("/projects", async (string? lang, IPortfolioService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GetProjectsAsync(lang ?? string.Empty, cancellationToken)))
            .WithName("GetProjects");

        group.MapGet("/experience", async (string? lang, IPortfolioService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GetExperienceAsync(lang ?? string.Empty, cancellationToken)))
            .WithName("GetExperience");

        return app;
    }
}
