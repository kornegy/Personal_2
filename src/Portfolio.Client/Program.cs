using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Client;
using Portfolio.Client.Localization;
using Portfolio.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// API живёт на том же адресе, что и статика, — отдельный хост не нужен.
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IPortfolioApi, PortfolioApiClient>();
builder.Services.AddScoped<LanguageState>();

var host = builder.Build();

// Язык определяем до первой отрисовки, иначе интерфейс мигнёт русской версией.
await host.Services.GetRequiredService<LanguageState>().InitializeAsync();

await host.RunAsync();
