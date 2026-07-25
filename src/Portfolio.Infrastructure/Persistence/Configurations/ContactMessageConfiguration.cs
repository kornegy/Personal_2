using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Persistence.Configurations;

internal sealed class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
{
    public void Configure(EntityTypeBuilder<ContactMessage> builder)
    {
        builder.ToTable("ContactMessages");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.SenderName).HasMaxLength(80).IsRequired();
        builder.Property(m => m.SenderEmail).HasMaxLength(160).IsRequired();
        builder.Property(m => m.Subject).HasMaxLength(120).IsRequired();
        builder.Property(m => m.Body).HasMaxLength(2000).IsRequired();
        builder.Property(m => m.SenderIpHash).HasMaxLength(64).IsRequired();

        // Индекс под проверку лимита сообщений от одного отправителя.
        builder.HasIndex(m => new { m.SenderIpHash, m.CreatedAtUtc });
    }
}
