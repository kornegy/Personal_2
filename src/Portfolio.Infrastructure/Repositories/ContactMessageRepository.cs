using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Abstractions;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Persistence;

namespace Portfolio.Infrastructure.Repositories;

/// <inheritdoc cref="IContactMessageRepository" />
internal sealed class ContactMessageRepository(PortfolioDbContext context) : IContactMessageRepository
{
    public async Task AddAsync(ContactMessage message, CancellationToken cancellationToken = default)
    {
        context.ContactMessages.Add(message);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<int> CountSinceAsync(
        string senderIpHash,
        DateTimeOffset since,
        CancellationToken cancellationToken = default) =>
        context.ContactMessages
            .AsNoTracking()
            .CountAsync(m => m.SenderIpHash == senderIpHash && m.CreatedAtUtc >= since, cancellationToken);
}
