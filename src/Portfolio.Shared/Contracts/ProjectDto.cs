namespace Portfolio.Shared.Contracts;

/// <summary>Карточка работы в портфолио.</summary>
public record ProjectDto(
    int Id,
    string Title,
    string Summary,
    string Description,
    string? ImageUrl,
    string? DemoUrl,
    string? SourceUrl,
    int Year,
    IReadOnlyList<string> Technologies);
