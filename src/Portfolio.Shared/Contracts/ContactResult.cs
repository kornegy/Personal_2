namespace Portfolio.Shared.Contracts;

/// <summary>Ответ на отправку формы обратной связи.</summary>
public record ContactResult(bool Success, string Message)
{
    public static ContactResult Accepted() =>
        new(true, "Сообщение отправлено. Отвечу в течение рабочего дня.");

    public static ContactResult Failed(string message) => new(false, message);
}
