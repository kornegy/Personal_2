using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence.Configurations;

internal sealed class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.ToTable("Experiences");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.LanguageCode).HasMaxLength(2).IsRequired();
        builder.Property(e => e.Company).HasMaxLength(120).IsRequired();
        builder.Property(e => e.Position).HasMaxLength(120).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(2000).IsRequired();

        builder.HasIndex(e => new { e.LanguageCode, e.SortOrder });
    }
}
