namespace Portfolio.Shared.Contracts;

/// <summary>Группа технологий со списком названий.</summary>
public record SkillCategoryDto(string Name, string Icon, IReadOnlyList<string> Skills);
