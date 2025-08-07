using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
namespace EjadaTraineesManagementSystem.Filters;
public class LogActionFilter : IActionFilter
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];

        _logger.LogInformation("Starting execution of {Controller}/{Action}", controller, action);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];

        if (context.Exception == null)
        {
            _logger.LogInformation("Finished execution of {Controller}/{Action} successfully", controller, action);
        }
        else
        {
            _logger.LogError(context.Exception, "Exception occurred in {Controller}/{Action}", controller, action);
        }
    }
}
