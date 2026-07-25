using Portfolio.Application.Abstractions;

namespace Portfolio.Api.Endpoints;

/// <summary>Только чтение: содержимое сайта. Ответы кэшируются, база не дёргается на каждый заход.</summary>
public static class PortfolioEndpoints
{
    public const string CachePolicy = "portfolio-content";

    public static IEndpointRouteBuilder MapPortfolioEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api").CacheOutput(CachePolicy);

        group.MapGet("/profile", async (IPortfolioService service, CancellationToken cancellationToken) =>
        {
            var profile = await service.GetProfileAsync(cancellationToken);
            return profile is null ? Results.NotFound() : Results.Ok(profile);
        })
        .WithName("GetProfile");

        group.MapGet("/skills", async (IPortfolioService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GetSkillsAsync(cancellationToken)))
            .WithName("GetSkills");

        group.MapGet("/projects", async (IPortfolioService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GetProjectsAsync(cancellationToken)))
            .WithName("GetProjects");

        group.MapGet("/experience", async (IPortfolioService service, CancellationToken cancellationToken) =>
            Results.Ok(await service.GetExperienceAsync(cancellationToken)))
            .WithName("GetExperience");

        return app;
    }
}
