using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Portfolio.Shared.Contracts;

namespace Portfolio.Api.Extensions;

/// <summary>
/// Ограничение частоты запросов — защита от перебора и от заваливания формы обратной связи.
/// Считается отдельно по каждому IP-адресу.
/// </summary>
public static class RateLimitingExtensions
{
    /// <summary>Имя политики для формы обратной связи.</summary>
    public const string ContactPolicy = "contact";

    public static IServiceCollection AddPortfolioRateLimiting(this IServiceCollection services) =>
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            // Общий лимит на всё приложение: спокойный просмотр сайта в него не упирается.
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    GetClientKey(context),
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 120,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0
                    }));

            // Отдельный, более строгий лимит на отправку формы.
            options.AddPolicy(ContactPolicy, context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    GetClientKey(context),
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 3,
                        Window = TimeSpan.FromMinutes(10),
                        QueueLimit = 0
                    }));

            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.ContentType = "application/json";
                // Отвечаем тем же контрактом, что и обычный ответ формы:
                // текст подставит клиент на нужном языке.
                await context.HttpContext.Response.WriteAsJsonAsync(
                    ContactResult.Failed(ContactResultCodes.RateLimited),
                    cancellationToken);
            };
        });

    private static string GetClientKey(HttpContext context) =>
        context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
}
