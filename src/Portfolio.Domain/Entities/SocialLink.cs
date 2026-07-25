namespace Portfolio.Domain.Entities;

/// <summary>Ссылка на внешний профиль: GitHub, LinkedIn, Telegram и т.д.</summary>
public class SocialLink
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    /// <summary>Имя иконки Bootstrap Icons без префикса, например «github».</summary>
    public string Icon { get; set; } = string.Empty;

    public int SortOrder { get; set; }

    public int ProfileId { get; set; }

    public Profile? Profile { get; set; }
}
