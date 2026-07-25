using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Persistence;

namespace Portfolio.Infrastructure.Repositories;

/// <summary>
/// Чтение содержимого сайта. Запросы только на чтение, поэтому везде AsNoTracking.
/// </summary>
internal sealed class PortfolioRepository(PortfolioDbContext context) : IPortfolioRepository
{
    public Task<Profile?> GetProfileAsync(CancellationToken cancellationToken = default) =>
        context.Profiles
            .AsNoTracking()
            .Include(p => p.SocialLinks)
            .OrderBy(p => p.Id)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<SkillCategory>> GetSkillCategoriesAsync(CancellationToken cancellationToken = default) =>
        await context.SkillCategories
            .AsNoTracking()
            .Include(c => c.Skills)
            .OrderBy(c => c.SortOrder)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Project>> GetProjectsAsync(CancellationToken cancellationToken = default) =>
        await context.Projects
            .AsNoTracking()
            .Include(p => p.Technologies)
            .OrderBy(p => p.SortOrder)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Experience>> GetExperiencesAsync(CancellationToken cancellationToken = default) =>
        await context.Experiences
            .AsNoTracking()
            .OrderBy(e => e.SortOrder)
            .ToListAsync(cancellationToken);
}
