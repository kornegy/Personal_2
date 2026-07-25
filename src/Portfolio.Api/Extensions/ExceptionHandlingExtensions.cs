using Microsoft.AspNetCore.Diagnostics;

namespace Portfolio.Api.Extensions;

/// <summary>
/// Единая обработка необработанных исключений: клиенту уходит нейтральный JSON,
/// подробности остаются в логах сервера. Так текст ошибки не подсказывает атакующему детали.
/// </summary>
public static class ExceptionHandlingExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this WebApplication app) =>
        app.UseExceptionHandler(builder => builder.Run(async context =>
        {
            var feature = context.Features.Get<IExceptionHandlerFeature>();
            var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("UnhandledException");
            logger.LogError(feature?.Error, "Необработанная ошибка при обработке {Path}", context.Request.Path);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                success = false,
                message = "Внутренняя ошибка сервера. Попробуйте позже."
            });
        }));
}
