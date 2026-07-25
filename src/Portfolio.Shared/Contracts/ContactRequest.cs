using System.ComponentModel.DataAnnotations;

namespace Portfolio.Shared.Contracts;

/// <summary>
/// Форма обратной связи. Атрибуты валидации работают и в браузере (EditForm),
/// и на сервере — данные из браузера всё равно перепроверяются.
///
/// В ErrorMessage лежит не текст, а ключ: сайт двуязычный, поэтому текст ошибки
/// подставляется на клиенте по выбранному языку (см. <c>LocalizedValidation</c>).
/// </summary>
public class ContactRequest
{
    [Required(ErrorMessage = ValidationKeys.NameRequired)]
    [StringLength(80, MinimumLength = 2, ErrorMessage = ValidationKeys.NameLength)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = ValidationKeys.EmailRequired)]
    [EmailAddress(ErrorMessage = ValidationKeys.EmailInvalid)]
    [StringLength(160, ErrorMessage = ValidationKeys.EmailLength)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = ValidationKeys.SubjectRequired)]
    [StringLength(120, MinimumLength = 3, ErrorMessage = ValidationKeys.SubjectLength)]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = ValidationKeys.MessageRequired)]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = ValidationKeys.MessageLength)]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Honeypot: поле скрыто от человека через CSS. Боты заполняют все поля подряд,
    /// поэтому непустое значение — признак спама.
    /// </summary>
    public string? Website { get; set; }
}

/// <summary>Ключи сообщений валидации. Значения обязаны быть константами — их требуют атрибуты.</summary>
public static class ValidationKeys
{
    public const string NameRequired = "validation.name.required";
    public const string NameLength = "validation.name.length";
    public const string EmailRequired = "validation.email.required";
    public const string EmailInvalid = "validation.email.invalid";
    public const string EmailLength = "validation.email.length";
    public const string SubjectRequired = "validation.subject.required";
    public const string SubjectLength = "validation.subject.length";
    public const string MessageRequired = "validation.message.required";
    public const string MessageLength = "validation.message.length";
}
