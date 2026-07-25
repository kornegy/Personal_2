using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Portfolio.Infrastructure.Persistence;

/// <summary>
/// Создаёт базу при первом запуске и наполняет её содержимым из <see cref="PortfolioSeedData"/>.
/// Данные добавляются только в пустую базу, поэтому повторные запуски ничего не дублируют.
/// </summary>
public sealed class DatabaseInitializer(PortfolioDbContext context, ILogger<DatabaseInitializer> logger)
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        var created = await context.Database.EnsureCreatedAsync(cancellationToken);
        if (created)
        {
            logger.LogInformation("База данных создана");
        }

        if (await context.Profiles.AnyAsync(cancellationToken))
        {
            return;
        }

        logger.LogInformation("Заполнение базы начальными данными");

        context.Profiles.Add(PortfolioSeedData.CreateProfile());
        context.SkillCategories.AddRange(PortfolioSeedData.CreateSkillCategories());
        context.Projects.AddRange(PortfolioSeedData.CreateProjects());
        context.Experiences.AddRange(PortfolioSeedData.CreateExperiences());

        await context.SaveChangesAsync(cancellationToken);
    }
}
