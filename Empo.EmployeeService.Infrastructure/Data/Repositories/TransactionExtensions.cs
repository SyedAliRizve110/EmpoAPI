using Microsoft.AspNetCore.Builder;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public static class TransactionExtensions
{
    public static IApplicationBuilder UseDbTransaction(this IApplicationBuilder app)
        => app.UseMiddleware<DbTransactionMiddleware>();
}
