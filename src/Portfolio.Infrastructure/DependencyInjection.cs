using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Application.Abstractions;
using Portfolio.Infrastructure.Persistence;
using Portfolio.Infrastructure.Repositories;

namespace Portfolio.Infrastructure;

/// <summary>Регистрация базы данных и хранилищ.</summary>
public static class DependencyInjection
{
    private const string ConnectionStringName = "PortfolioDatabase";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName)
            ?? throw new InvalidOperationException(
                $"Не задана строка подключения «{ConnectionStringName}» в конфигурации.");

        services.AddDbContext<PortfolioDbContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
        services.AddScoped<DatabaseInitializer>();

        return services;
    }

    /// <summary>Создаёт и наполняет базу. Вызывается один раз при старте приложения.</summary>
    public static async Task InitializeDatabaseAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
        await initializer.InitializeAsync(cancellationToken);
    }
}
