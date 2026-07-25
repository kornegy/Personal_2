namespace Portfolio.Domain.Entities;

/// <summary>
/// Главная информация обо мне: то, что видит посетитель в первом экране и в блоке «Обо мне».
/// В базе всегда одна запись.
/// </summary>
public class Profile
{
    public int Id { get; set; }

    /// <summary>Имя и фамилия.</summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>Должность, например «Front-End React разработчик».</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Короткий слоган под именем (одно предложение).</summary>
    public string Headline { get; set; } = string.Empty;

    /// <summary>Развёрнутый текст блока «Обо мне».</summary>
    public string About { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? Phone { get; set; }

    /// <summary>Путь к фотографии внутри wwwroot, например «img/avatar.jpg».</summary>
    public string? PhotoUrl { get; set; }

    /// <summary>Путь к PDF-резюме внутри wwwroot, например «files/cv.pdf».</summary>
    public string? ResumeUrl { get; set; }

    /// <summary>Год начала коммерческой работы — из него считается опыт.</summary>
    public int CareerStartYear { get; set; }

    public ICollection<SocialLink> SocialLinks { get; set; } = new List<SocialLink>();
}
