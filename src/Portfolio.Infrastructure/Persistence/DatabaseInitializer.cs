using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portfolio.Infrastructure.Persistence.Seed;

namespace Portfolio.Infrastructure.Persistence;

/// <summary>
/// Приводит базу к актуальной схеме через миграции EF Core и наполняет её содержимым.
/// Данные добавляются только в пустую базу, поэтому повторные запуски ничего не дублируют.
/// </summary>
public sealed class DatabaseInitializer(PortfolioDbContext context, ILogger<DatabaseInitializer> logger)
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        var pending = (await context.Database.GetPendingMigrationsAsync(cancellationToken)).ToList();
        if (pending.Count > 0)
        {
            logger.LogInformation("Применяем миграции: {Migrations}", string.Join(", ", pending));
            await context.Database.MigrateAsync(cancellationToken);
        }

        if (await context.Profiles.AnyAsync(cancellationToken))
        {
            return;
        }

        logger.LogInformation("Заполнение базы начальными данными");

        context.Profiles.AddRange(PortfolioSeedData.CreateProfiles());
        context.SkillCategories.AddRange(PortfolioSeedData.CreateSkillCategories());
        context.Projects.AddRange(PortfolioSeedData.CreateProjects());
        context.Experiences.AddRange(PortfolioSeedData.CreateExperiences());

        await context.SaveChangesAsync(cancellationToken);
    }
}
