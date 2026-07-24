using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyPersonal.Server.Data;
using MyPersonal.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=app.db"));

var app = builder.Build();

// Create + seed the SQLite database on startup.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.EnsureSeeded(db);
}

// Serve the Blazor WebAssembly client.
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

var api = app.MapGroup("/api");

// Everything the homepage needs in a single round-trip.
api.MapGet("/site", async (AppDbContext db) =>
{
    var profile = await db.Profiles.AsNoTracking().FirstOrDefaultAsync() ?? new Profile();
    var visits = await db.Stats.Where(s => s.Key == "visits").Select(s => s.Value).FirstOrDefaultAsync();

    return Results.Ok(new SiteData
    {
        Profile = profile,
        Services = await db.Services.AsNoTracking().OrderBy(s => s.SortOrder).ToListAsync(),
        Skills = await db.Skills.AsNoTracking().OrderBy(s => s.SortOrder).ToListAsync(),
        Experience = await db.Experience.AsNoTracking().OrderBy(e => e.SortOrder).ToListAsync(),
        Projects = await db.Projects.AsNoTracking().OrderBy(p => p.SortOrder).ToListAsync(),
        Visits = visits,
    });
});

// Increment and return the visit counter.
api.MapPost("/visit", async (AppDbContext db) =>
{
    var stat = await db.Stats.FirstOrDefaultAsync(s => s.Key == "visits");
    if (stat is null)
    {
        stat = new SiteStat { Key = "visits", Value = 0 };
        db.Stats.Add(stat);
    }
    stat.Value++;
    await db.SaveChangesAsync();
    return Results.Ok(new { visits = stat.Value });
});

// Store a message from the contact form.
api.MapPost("/contact", async (ContactMessage message, AppDbContext db) =>
{
    var ctx = new ValidationContext(message);
    var errors = new List<ValidationResult>();
    if (!Validator.TryValidateObject(message, ctx, errors, validateAllProperties: true))
        return Results.ValidationProblem(errors.ToDictionary(
            e => e.MemberNames.FirstOrDefault() ?? "", e => new[] { e.ErrorMessage ?? "Invalid" }));

    message.Id = 0;
    message.CreatedUtc = DateTime.UtcNow;
    db.Messages.Add(message);
    await db.SaveChangesAsync();
    return Results.Ok(new { ok = true });
});

app.MapFallbackToFile("index.html");

app.Run();
