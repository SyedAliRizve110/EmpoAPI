using Empo.BuildingBlocks.Application;
using Empo.BuildingBlocks.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Empo.EmloyeeService.Api.Configuration;

public class HttpResponseAxceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        try
        {
            if (context.Exception != null)
            {
                throw context.Exception;
            }
            if (context.HttpContext.Request.Method.Equals("Get", StringComparison.CurrentCultureIgnoreCase) == false)
            {
                IUnitOfWork unitOfWork = context.HttpContext.RequestServices.GetService<IUnitOfWork>();
                var task = Task.Run(async () => await unitOfWork.CommitAsync());
                task.Wait();
            }
        }
        catch (Exception ex)
        {

            context.Exception = ex.InnerException != null ? ex.InnerException : ex;
            context.Result = null;
        }
        ActionResult result = GetResult(context);
    }
    private ActionResult GetResult(ActionExecutedContext context)
    {
        ApiResponse response = new();
        if (context.Result is OkObjectResult okResult)
        {
            response = new(okResult.StatusCode, okResult.Value, null);
        }
        else if (context.Exception is HttpResponseException httpRequestException)
        {
            var error = new ApiResponse.ErrorDetails() { Message = httpRequestException.Message, ErrorCode = 0 };
            var errors = new List<ApiResponse.ErrorDetails>() { error };
            response = new(StatusCodes.Status500InternalServerError, null, errors);
            context.ExceptionHandled = true;
        }
        else if(context.Exception is Exception exception)
        {
            var error = new ApiResponse.ErrorDetails() { Message = exception.Message, ErrorCode = 0 };
            var errors = new List<ApiResponse.ErrorDetails>() { error };
            response = new(StatusCodes.Status500InternalServerError, null, errors);
            context.ExceptionHandled = true;

        }

        ActionResult result = new(response);
        return result;
    }
}
