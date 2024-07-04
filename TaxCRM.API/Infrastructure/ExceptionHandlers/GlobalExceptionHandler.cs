using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TaxCRM.API.Infrastructure.ExceptionHandlers;

public class GlobalExceptionHandler(IHostEnvironment env) : IExceptionHandler
{
    private const string unhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var problemDetails = GetProblemDetails(httpContext, exception);

        const string contentType = "application/json";
        httpContext.Response.ContentType = contentType;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private ProblemDetails GetProblemDetails(HttpContext httpContext, Exception exception)
    {
        var problemDetails = new ProblemDetails()
        {
            Status = httpContext.Response.StatusCode,
            Title = unhandledExceptionMsg,
        };

        if (env.IsStaging() || env.IsProduction())
                return problemDetails;

        problemDetails.Detail = exception.ToString();
        problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
        problemDetails.Extensions["data"] = exception.Data;

        return problemDetails;
    } 
}
