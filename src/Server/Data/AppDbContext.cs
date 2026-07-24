using Microsoft.EntityFrameworkCore;
using MyPersonal.Shared;

namespace MyPersonal.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ExperienceItem> Experience => Set<ExperienceItem>();
    public DbSet<SkillCategory> Skills => Set<SkillCategory>();
    public DbSet<ServiceOffering> Services => Set<ServiceOffering>();
    public DbSet<ContactMessage> Messages => Set<ContactMessage>();
    public DbSet<SiteStat> Stats => Set<SiteStat>();
}

/// <summary>Single-row key/value counter table (e.g. total visits).</summary>
public class SiteStat
{
    public int Id { get; set; }
    public string Key { get; set; } = "";
    public long Value { get; set; }
}
