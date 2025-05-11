using Microsoft.AspNetCore.Mvc.Filters;
using Empo.BuildingBlocks.Application;

namespace Empo.EmloyeeService.Api.Configuration
{
    public class HttpResponseAxceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;
        public void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                if (context.Exception!= null)
                {
                    throw context.Exception;
                }
                if (context.HttpContext.Request.Method.Equals("Get", StringComparison.CurrentCultureIgnoreCase) == false)
                {
                  //  IUnitOfWork 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
