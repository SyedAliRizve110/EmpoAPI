using Empo.EmployeeService.Infrastructure;
using Empo.MediaService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Empo.MediaService.Api;

public static class ServiceExtensions
{
    public static void InitializeDataBase(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var IDContext = scope.ServiceProvider.GetRequiredService<MediaContext>();
            IDContext.Database.Migrate();
        }
    }
}