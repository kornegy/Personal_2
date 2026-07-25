namespace Portfolio.Domain.Entities;

/// <summary>Место работы или крупный контракт — строка в таймлайне опыта.</summary>
public class Experience
{
    public int Id { get; set; }

    public string Company { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;

    /// <summary>Ключевые задачи и результаты, по одному пункту на строку.</summary>
    public string Description { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    /// <summary>null — текущее место работы.</summary>
    public DateOnly? EndDate { get; set; }

    public int SortOrder { get; set; }
}
