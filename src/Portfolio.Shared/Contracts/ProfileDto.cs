namespace Portfolio.Shared.Contracts;

/// <summary>Данные первого экрана и блока «Обо мне».</summary>
public record ProfileDto(
    string FullName,
    string Title,
    string Headline,
    string About,
    string Location,
    string Email,
    string? Phone,
    string? PhotoUrl,
    string? ResumeUrl,
    int YearsOfExperience,
    IReadOnlyList<SocialLinkDto> SocialLinks);

/// <summary>Ссылка на внешний профиль.</summary>
public record SocialLinkDto(string Name, string Url, string Icon);
