namespace Portfolio.Domain.Entities;

/// <summary>Отдельная технология внутри категории.</summary>
public class Skill
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public int SkillCategoryId { get; set; }

    public SkillCategory? Category { get; set; }
}
