using Arale.CodeGen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Arale.CodeGen.Api.Filters;

/// <summary>
/// </summary>
public class RequestParamValidationFilter : IActionFilter
{
    /// <inheritdoc />
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;

        var errorMsg = context.ModelState.Values.SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .FirstOrDefault();
        context.Result = new OkObjectResult(ApiResult<string>.Fail(errorMsg));
    }

    /// <inheritdoc />
    public void OnActionExecuted(ActionExecutedContext context)
    {
        // do nothing
    }
}
