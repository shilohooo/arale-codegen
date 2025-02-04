using Arale.CodeGen.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace Arale.CodeGen.Api.Handlers;

/// <summary>
/// </summary>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <inheritdoc />
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception has occurred: {Message}", exception.Message);
        await httpContext.Response.WriteAsJsonAsync(ApiResult<string>.Fail(exception.Message), cancellationToken);
        return true;
    }
}
