using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence.Configurations;

internal sealed class SkillCategoryConfiguration : IEntityTypeConfiguration<SkillCategory>
{
    public void Configure(EntityTypeBuilder<SkillCategory> builder)
    {
        builder.ToTable("SkillCategories");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.LanguageCode).HasMaxLength(2).IsRequired();
        builder.Property(c => c.Name).HasMaxLength(80).IsRequired();
        builder.Property(c => c.Icon).HasMaxLength(60).IsRequired();

        builder.HasIndex(c => new { c.LanguageCode, c.SortOrder });

        builder.HasMany(c => c.Skills)
            .WithOne(s => s.Category!)
            .HasForeignKey(s => s.SkillCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

internal sealed class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name).HasMaxLength(60).IsRequired();
        builder.HasIndex(s => new { s.SkillCategoryId, s.SortOrder });
    }
}
