using System.ComponentModel.DataAnnotations;

namespace Portfolio.Shared.Contracts;

/// <summary>
/// Форма обратной связи. Атрибуты валидации работают и в браузере (EditForm),
/// и на сервере — данные из браузера всё равно перепроверяются.
/// </summary>
public class ContactRequest
{
    [Required(ErrorMessage = "Укажите имя")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 80 символов")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    [StringLength(160, ErrorMessage = "Email слишком длинный")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите тему")]
    [StringLength(120, MinimumLength = 3, ErrorMessage = "Тема должна быть от 3 до 120 символов")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Напишите сообщение")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Сообщение должно быть от 10 до 2000 символов")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Honeypot: поле скрыто от человека через CSS. Боты заполняют все поля подряд,
    /// поэтому непустое значение — признак спама.
    /// </summary>
    public string? Website { get; set; }
}
