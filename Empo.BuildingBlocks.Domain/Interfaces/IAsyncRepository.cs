using System.Linq.Expressions;

namespace Empo.BuildingBlocks.Domain.Interfaces;

public interface IAsyncRepository<T> where T : IDomainBase
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    IQueryable<T> Entity {  get; }
    Task<T> GetByIdAsync(Guid Id);
    Task<T> GetAsync(Expression<Func<T, bool>> expression);

    Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);

    Task<PagedList<T>> GetAllAsync(PageParams param);
    IQueryable<T> GetDBSet();
}
