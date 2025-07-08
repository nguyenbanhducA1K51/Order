namespace API.Filler;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
//This RequestLoggingFilter is a custom action filter designed to log HTTP request information in an ASP.NET Core application.
public class RequestLoggingFilter : IActionFilter
{
    private readonly ILogger<RequestLoggingFilter> _logger;

    public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Log before the action executes
        _logger.LogInformation("Incoming Request: {Method} {Path}", 
            context.HttpContext.Request.Method,
            context.HttpContext.Request.Path);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Log after the action executes
        _logger.LogInformation("Completed Request: {Method} {Path} => {StatusCode}",
            context.HttpContext.Request.Method,
            context.HttpContext.Request.Path,
            context.HttpContext.Response.StatusCode);
    }
}