using System.ComponentModel.DataAnnotations;

namespace Portfolio.Api.Filters;

/// <summary>
/// Проверяет тело запроса по атрибутам из Portfolio.Shared.
/// Правила описаны один раз и применяются и в браузере, и здесь: данные, пришедшие
/// от клиента, всегда считаются недоверенными.
/// </summary>
public sealed class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var model = context.Arguments.OfType<T>().FirstOrDefault();
        if (model is null)
        {
            return Results.BadRequest(new { success = false, message = "Тело запроса отсутствует или не распознано." });
        }

        var results = new List<ValidationResult>();
        var valid = Validator.TryValidateObject(model, new ValidationContext(model), results, validateAllProperties: true);
        if (valid)
        {
            return await next(context);
        }

        var errors = results
            .SelectMany(result => result.MemberNames.DefaultIfEmpty(string.Empty),
                (result, member) => new { Member = member, result.ErrorMessage })
            .GroupBy(item => item.Member)
            .ToDictionary(
                group => group.Key,
                group => group.Select(item => item.ErrorMessage ?? "Некорректное значение").ToArray());

        return Results.ValidationProblem(errors);
    }
}
