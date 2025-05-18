using Microsoft.AspNetCore.Http;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public class DbTransactionMiddleware
{
    private readonly RequestDelegate _next;

    public DbTransactionMiddleware(RequestDelegate next)
    {
            _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        if (httpContext.Request.Method.Equals("GET", StringComparison.CurrentCultureIgnoreCase))
        {
            await _next(httpContext);
            return;
        }
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception Log: " + ex.Message);
            throw ex;
        }
    }
}
