using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Persistence;

namespace Portfolio.Infrastructure.Repositories;

/// <summary>
/// Чтение содержимого сайта на конкретном языке.
/// Запросы только на чтение, поэтому везде AsNoTracking.
/// </summary>
internal sealed class PortfolioRepository(PortfolioDbContext context) : IPortfolioRepository
{
    public Task<Profile?> GetProfileAsync(string languageCode, CancellationToken cancellationToken = default) =>
        context.Profiles
            .AsNoTracking()
            .Include(p => p.SocialLinks)
            .Where(p => p.LanguageCode == languageCode)
            .OrderBy(p => p.Id)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<SkillCategory>> GetSkillCategoriesAsync(string languageCode, CancellationToken cancellationToken = default) =>
        await context.SkillCategories
            .AsNoTracking()
            .Include(c => c.Skills)
            .Where(c => c.LanguageCode == languageCode)
            .OrderBy(c => c.SortOrder)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Project>> GetProjectsAsync(string languageCode, CancellationToken cancellationToken = default) =>
        await context.Projects
            .AsNoTracking()
            .Include(p => p.Technologies)
            .Where(p => p.LanguageCode == languageCode)
            .OrderBy(p => p.SortOrder)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Experience>> GetExperiencesAsync(string languageCode, CancellationToken cancellationToken = default) =>
        await context.Experiences
            .AsNoTracking()
            .Where(e => e.LanguageCode == languageCode)
            .OrderBy(e => e.SortOrder)
            .ToListAsync(cancellationToken);
}
