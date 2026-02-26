using Empo.BuildingBlocks.Domain;
using Empo.BuildingBlocks.Infrastructure.Data;
using System.Linq.Expressions;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories.Interfaces;

public interface IRepository<T>
    where T : EntityBase
{
    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    Task SaveAllAsync(IEnumerable<T> entities);

    Task DeleteAllAsync(IEnumerable<T> entities);

    Task<T> GetByIdAsync(Guid id, string?[] includes = null);

    Task<T> GetAsync(Expression<Func<T, bool>> expression);

    Task<List<T>> ListAsync(Expression<Func<T, bool>> expression, string?[] includes = null);

    Task<PagedList<T>> GetAllAsync(PageParams paramm);
}
