using Empo.BuildingBlocks.Application;
using Microsoft.AspNetCore.Mvc;

namespace Empo.EmloyeeService.Api.Configuration;

public class ActionResult : IActionResult
{
    private readonly ApiResponse _result;

    public ActionResult(ApiResponse result)
    {
        _result = result;
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(_result)
        {
            StatusCode = context.HttpContext.Response.StatusCode,
        };

        await objectResult.ExecuteResultAsync(context);
    }
}
