namespace Portfolio.Api.Middleware;

/// <summary>
/// Добавляет заголовки безопасности к каждому ответу.
/// Они закрывают самые массовые классы атак: XSS, clickjacking, MIME-sniffing, утечку Referer.
/// </summary>
public sealed class SecurityHeadersMiddleware(RequestDelegate next)
{
    /// <summary>
    /// Content-Security-Policy: браузер выполнит только те ресурсы, которые перечислены здесь.
    ///  - 'wasm-unsafe-eval' обязателен: без него не запустится среда .NET в браузере;
    ///  - 'unsafe-inline' в style-src нужен Blazor — он проставляет inline-стили компонентам;
    ///  - внешние адреса — это CDN Bootstrap и шрифт. Если раздавать их со своего домена,
    ///    список сокращается до 'self'.
    /// </summary>
    private const string ContentSecurityPolicy =
        "default-src 'self'; " +
        "base-uri 'self'; " +
        "object-src 'none'; " +
        "frame-ancestors 'none'; " +
        "form-action 'self'; " +
        "img-src 'self' data:; " +
        "script-src 'self' 'wasm-unsafe-eval'; " +
        "style-src 'self' 'unsafe-inline' https://cdn.jsdelivr.net https://fonts.googleapis.com; " +
        "font-src 'self' https://cdn.jsdelivr.net https://fonts.gstatic.com; " +
        "connect-src 'self'; " +
        "upgrade-insecure-requests";

    public async Task InvokeAsync(HttpContext context)
    {
        var headers = context.Response.Headers;

        headers["Content-Security-Policy"] = ContentSecurityPolicy;
        headers["X-Content-Type-Options"] = "nosniff";
        headers["X-Frame-Options"] = "DENY";
        headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
        headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=(), interest-cohort=()";
        headers["Cross-Origin-Opener-Policy"] = "same-origin";
        headers["Cross-Origin-Resource-Policy"] = "same-origin";

        await next(context);
    }
}

public static class SecurityHeadersMiddlewareExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app) =>
        app.UseMiddleware<SecurityHeadersMiddleware>();
}
