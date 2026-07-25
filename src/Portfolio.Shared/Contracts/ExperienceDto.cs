namespace Portfolio.Shared.Contracts;

/// <summary>Строка таймлайна опыта. Период уже отформатирован сервером.</summary>
public record ExperienceDto(
    string Company,
    string Position,
    string Period,
    bool IsCurrent,
    IReadOnlyList<string> Highlights);
