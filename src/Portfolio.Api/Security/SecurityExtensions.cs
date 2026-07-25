using System.Net;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;

namespace Portfolio.Api.Security;

/// <summary>Настройка HTTPS-редиректа и доверенных прокси.</summary>
public static class SecurityExtensions
{
    public static IServiceCollection AddPortfolioSecurity(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SecurityOptions.SectionName);
        services.Configure<SecurityOptions>(section);

        var options = section.Get<SecurityOptions>() ?? new SecurityOptions();

        services.AddHttpsRedirection(redirect =>
        {
            // 308 вместо 307: постоянный редирект браузеры и поисковики запоминают,
            // поэтому по http к нам повторно не ходят.
            redirect.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;

            if (options.HttpsPort is { } port)
            {
                redirect.HttpsPort = port;
            }
        });

        services.AddHsts(hsts =>
        {
            hsts.MaxAge = TimeSpan.FromDays(365);
            hsts.IncludeSubDomains = true;
        });

        if (UsesForwardedHeaders(options))
        {
            services.Configure<ForwardedHeadersOptions>(forwarded =>
            {
                forwarded.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

                // Списки по умолчанию разрешают только localhost — очищаем и задаём свои.
                forwarded.KnownProxies.Clear();
                forwarded.KnownNetworks.Clear();

                if (options.TrustAllProxies)
                {
                    // Пустые списки означают «доверять любому источнику».
                    // Допустимо только когда приложение доступно исключительно через прокси платформы.
                    return;
                }

                foreach (var proxy in options.KnownProxies)
                {
                    if (IPAddress.TryParse(proxy, out var address))
                    {
                        forwarded.KnownProxies.Add(address);
                    }
                }
            });
        }

        return services;
    }

    /// <summary>
    /// Включает чтение X-Forwarded-* только если прокси заданы в конфигурации.
    /// Вызывается до всех остальных middleware: они должны видеть уже исправленные схему и IP.
    /// </summary>
    public static IApplicationBuilder UseTrustedProxies(this WebApplication app)
    {
        var options = app.Services.GetRequiredService<IOptions<SecurityOptions>>().Value;
        if (UsesForwardedHeaders(options))
        {
            app.UseForwardedHeaders();
        }

        return app;
    }

    /// <summary>Перенаправление на https включается только если его не отключили явно.</summary>
    public static IApplicationBuilder UseHttpsRedirectionIfEnabled(this WebApplication app)
    {
        var options = app.Services.GetRequiredService<IOptions<SecurityOptions>>().Value;
        if (options.EnableHttpsRedirection)
        {
            app.UseHttpsRedirection();
        }

        return app;
    }

    private static bool UsesForwardedHeaders(SecurityOptions options) =>
        options.TrustAllProxies || options.KnownProxies.Count > 0;
}
