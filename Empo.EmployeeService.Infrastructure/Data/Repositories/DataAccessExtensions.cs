using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public static class DataAccessExtensions
{
    public static IQueryable<T> IncludeMultiple<T>( this IQueryable<T> query,
        params string[] includes) where T : class
    {
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }
        return query;
    }
}
