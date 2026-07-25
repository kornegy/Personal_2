using Microsoft.Net.Http.Headers;
using Portfolio.Api.Endpoints;
using Portfolio.Api.Extensions;
using Portfolio.Api.Middleware;
using Portfolio.Api.Security;
using Portfolio.Application;
using Portfolio.Application.Contact;
using Portfolio.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    // Сайт ничего не принимает от пользователя, кроме короткой формы,
    // поэтому размер тела запроса жёстко ограничен.
    options.Limits.MaxRequestBodySize = 64 * 1024;

    // Не сообщаем в заголовках, каким сервером обслуживается сайт.
    options.AddServerHeader = false;
});

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPortfolioRateLimiting();

// Кэш общий для всех посетителей, поэтому ключ обязан учитывать язык —
// иначе первый зашедший «зафиксирует» свою версию контента для остальных.
builder.Services.AddOutputCache(options =>
    options.AddPolicy(PortfolioEndpoints.CachePolicy, policy => policy
        .Expire(TimeSpan.FromMinutes(10))
        .SetVaryByQuery(PortfolioEndpoints.LanguageQueryParameter)));

builder.Services.AddPortfolioSecurity(builder.Configuration);

var app = builder.Build();

EnsureContactSaltConfigured(app);
await app.Services.InitializeDatabaseAsync();

// Должно идти первым: остальные middleware обязаны видеть настоящие
// схему и IP клиента, а не адрес reverse proxy.
app.UseTrustedProxies();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseGlobalExceptionHandling();

    // Говорит браузеру год ходить на сайт только по https, даже если
    // пользователь наберёт адрес вручную без протокола.
    app.UseHsts();
}

// Любой запрос по http получает постоянный редирект на https.
app.UseHttpsRedirection();
app.UseSecurityHeaders();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
    {
        // Файлы с хешем в имени не меняются, остальное перепроверяется браузером.
        context.Context.Response.Headers[HeaderNames.CacheControl] = "public, max-age=3600";
    }
});

// UseRouting обязателен до UseRateLimiter: иначе политика конкретного эндпоинта не применится.
app.UseRouting();
app.UseRateLimiter();
app.UseOutputCache();

app.MapPortfolioEndpoints();
app.MapContactEndpoints();

// Любой неизвестный путь отдаёт SPA — маршрутизацию дальше разбирает Blazor.
app.MapFallbackToFile("index.html");

app.Run();

// Соль для хеширования IP не должна оставаться пустой вне разработки:
// без неё хеш подбирается перебором диапазона адресов.
static void EnsureContactSaltConfigured(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        return;
    }

    var salt = app.Configuration[$"{ContactOptions.SectionName}:IpHashSalt"];
    if (string.IsNullOrWhiteSpace(salt) || salt.Length < 16)
    {
        throw new InvalidOperationException(
            "Не задана переменная окружения Contact__IpHashSalt (минимум 16 символов). " +
            "Она нужна для безопасного хеширования IP отправителей формы.");
    }
}
