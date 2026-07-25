using Portfolio.Api.Extensions;
using Portfolio.Api.Filters;
using Portfolio.Application.Abstractions;
using Portfolio.Shared.Contracts;

namespace Portfolio.Api.Endpoints;

/// <summary>Приём формы обратной связи.</summary>
public static class ContactEndpoints
{
    public static IEndpointRouteBuilder MapContactEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/contact", async (
                ContactRequest request,
                IContactService contactService,
                HttpContext httpContext,
                CancellationToken cancellationToken) =>
            {
                var senderIp = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                var result = await contactService.SubmitAsync(request, senderIp, cancellationToken);

                return result.Success
                    ? Results.Ok(result)
                    : Results.Json(result, statusCode: StatusCodes.Status429TooManyRequests);
            })
            .AddEndpointFilter<ValidationFilter<ContactRequest>>()
            .RequireRateLimiting(RateLimitingExtensions.ContactPolicy)
            .WithName("SubmitContactMessage");

        return app;
    }
}
