using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Portfolio.Infrastructure.Persistence;

/// <summary>
/// Нужна только инструменту dotnet ef: позволяет создавать миграции из этого проекта,
/// не запуская веб-приложение. На работу сайта не влияет.
/// </summary>
public sealed class PortfolioDbContextFactory : IDesignTimeDbContextFactory<PortfolioDbContext>
{
    public PortfolioDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<PortfolioDbContext>()
            .UseSqlite("Data Source=portfolio.design.db")
            .Options;

        return new PortfolioDbContext(options);
    }
}
