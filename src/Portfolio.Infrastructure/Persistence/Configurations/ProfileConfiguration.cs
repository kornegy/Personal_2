using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence.Configurations;

internal sealed class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.LanguageCode).HasMaxLength(2).IsRequired();
        builder.Property(p => p.FullName).HasMaxLength(120).IsRequired();
        builder.Property(p => p.Title).HasMaxLength(160).IsRequired();
        builder.Property(p => p.Headline).HasMaxLength(320).IsRequired();
        builder.Property(p => p.About).HasMaxLength(4000).IsRequired();
        builder.Property(p => p.Location).HasMaxLength(120).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(160).IsRequired();
        builder.Property(p => p.Phone).HasMaxLength(40);
        builder.Property(p => p.PhotoUrl).HasMaxLength(256);
        builder.Property(p => p.ResumeUrl).HasMaxLength(256);

        // На каждый язык допустим ровно один профиль.
        builder.HasIndex(p => p.LanguageCode).IsUnique();

        builder.HasMany(p => p.SocialLinks)
            .WithOne(l => l.Profile!)
            .HasForeignKey(l => l.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

internal sealed class SocialLinkConfiguration : IEntityTypeConfiguration<SocialLink>
{
    public void Configure(EntityTypeBuilder<SocialLink> builder)
    {
        builder.ToTable("SocialLinks");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Name).HasMaxLength(60).IsRequired();
        builder.Property(l => l.Url).HasMaxLength(300).IsRequired();
        builder.Property(l => l.Icon).HasMaxLength(60).IsRequired();

        builder.HasIndex(l => l.SortOrder);
    }
}
