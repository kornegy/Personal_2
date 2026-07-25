using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence;

/// <summary>Контекст базы данных SQLite.</summary>
public class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : DbContext(options)
{
    public DbSet<Profile> Profiles => Set<Profile>();

    public DbSet<SocialLink> SocialLinks => Set<SocialLink>();

    public DbSet<SkillCategory> SkillCategories => Set<SkillCategory>();

    public DbSet<Skill> Skills => Set<Skill>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<ProjectTechnology> ProjectTechnologies => Set<ProjectTechnology>();

    public DbSet<Experience> Experiences => Set<Experience>();

    public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Конфигурации вынесены в отдельные классы — по одному на сущность.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortfolioDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
