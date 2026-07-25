using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Portfolio.Application.Abstractions;
using Portfolio.Application.Contact;
using Portfolio.Application.Services;

namespace Portfolio.Application;

/// <summary>Регистрация сценариев приложения в контейнере.</summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton(TimeProvider.System);

        services.AddOptions<ContactOptions>()
            .Bind(configuration.GetSection(ContactOptions.SectionName))
            .Validate(o => o.FloodWindowMinutes > 0, "FloodWindowMinutes должен быть больше нуля")
            .Validate(o => o.MaxMessagesPerWindow > 0, "MaxMessagesPerWindow должен быть больше нуля");

        services.AddScoped<IPortfolioService, PortfolioService>();
        services.AddScoped<IContactService, ContactService>();

        return services;
    }
}
