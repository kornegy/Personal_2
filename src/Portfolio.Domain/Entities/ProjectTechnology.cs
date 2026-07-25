namespace Portfolio.Domain.Entities;

/// <summary>Технология, использованная в проекте (тег на карточке).</summary>
public class ProjectTechnology
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public int ProjectId { get; set; }

    public Project? Project { get; set; }
}
