namespace Portfolio.Domain.Entities;

/// <summary>Группа технологий, например «Frontend» или «Инструменты».</summary>
public class SkillCategory
{
    public int Id { get; set; }

    /// <summary>Код языка: «ru» или «en».</summary>
    public string LanguageCode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    /// <summary>Имя иконки Bootstrap Icons без префикса, например «code-slash».</summary>
    public string Icon { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
