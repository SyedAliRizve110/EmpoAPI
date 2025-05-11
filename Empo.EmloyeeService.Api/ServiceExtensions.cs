using Empo.EmployeeService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmloyeeService.Api;

public static class ServiceExtensions
{
    public static void InitializeDataBase(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var IDContext = scope.ServiceProvider.GetRequiredService<EmployeeContext>();
            IDContext.Database.Migrate();
        }
    }
}