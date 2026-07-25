using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence.Configurations;

internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title).HasMaxLength(120).IsRequired();
        builder.Property(p => p.Summary).HasMaxLength(300).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(2000).IsRequired();
        builder.Property(p => p.ImageUrl).HasMaxLength(256);
        builder.Property(p => p.DemoUrl).HasMaxLength(300);
        builder.Property(p => p.SourceUrl).HasMaxLength(300);

        builder.HasIndex(p => p.SortOrder);

        builder.HasMany(p => p.Technologies)
            .WithOne(t => t.Project!)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

internal sealed class ProjectTechnologyConfiguration : IEntityTypeConfiguration<ProjectTechnology>
{
    public void Configure(EntityTypeBuilder<ProjectTechnology> builder)
    {
        builder.ToTable("ProjectTechnologies");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name).HasMaxLength(60).IsRequired();
        builder.HasIndex(t => new { t.ProjectId, t.SortOrder });
    }
}
