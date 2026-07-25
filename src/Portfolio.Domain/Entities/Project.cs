namespace Portfolio.Domain.Entities;

/// <summary>Работа в портфолио.</summary>
public class Project
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    /// <summary>Одна строка для карточки.</summary>
    public string Summary { get; set; } = string.Empty;

    /// <summary>Что было сделано и какой получился результат.</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>Путь к превью внутри wwwroot, например «img/projects/shop.jpg».</summary>
    public string? ImageUrl { get; set; }

    public string? DemoUrl { get; set; }

    public string? SourceUrl { get; set; }

    /// <summary>Год завершения — показывается на карточке.</summary>
    public int Year { get; set; }

    public int SortOrder { get; set; }

    public ICollection<ProjectTechnology> Technologies { get; set; } = new List<ProjectTechnology>();
}
