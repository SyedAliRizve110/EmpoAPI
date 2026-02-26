using Empo.BuildingBlocks.Domain;
using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Utility.Extension;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public class GenericRepository<T> : IRepository<T> where T : EntityBase
{

    public readonly DbSet<T> _dbSet;
    protected EmployeeContext _dbContext { get; set; }

    public GenericRepository(EmployeeContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
        _dbContext = dbContext;
    }
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteAllAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
        }
        await Task.Run(() =>
        {
            _dbSet.RemoveRange(entities);
        });
    }

    public async Task DeleteAsync(T entity)
    {
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        await Task.Run(() =>
        {
            _dbSet.Remove(entity);
        });
    }

    public Task<PagedList<T>> GetAllAsync(PageParams paramm)
    {
        return PagedList<T>.ToPagedList(_dbSet,
           paramm.PageNumber,
           paramm.PageSize
           );
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.FirstOrDefaultAsync(expression);
    }

    public async Task<T> GetByIdAsync(Guid id, string?[] includes = null)
    {
        includes = includes ?? new string[0];
        var entity = _dbSet.AsNoTracking()
            .Where(x => x.Id == id)
            .IncludeMultiple(includes).FirstOrDefault();

        return entity;
    }

    public Task<List<T>> ListAsync(Expression<Func<T, bool>> expression, string?[] includes = null)
    {
        includes = includes ?? new string[0];
        return _dbSet.AsNoTracking()
            .Where(expression)
            .IncludeMultiple(includes)
            .ToListAsync();
    }

    public async Task SaveAllAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (GuidExtensions.IsNulllOrEmptyGuid(entity.Id) == true)
            {
                await _dbSet.AddAsync(entity);
            }
            else
            {
                _dbSet.Update(entity);
            }
        }
    }

    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.FromResult(entity);
    }
}
