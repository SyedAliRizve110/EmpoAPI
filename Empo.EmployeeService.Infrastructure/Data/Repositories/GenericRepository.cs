using Empo.BuildingBlocks.Domain;
using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Repositories.Interfaces;
using Empo.Shared.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public class GenericRepository<T> : IRepository<T> where T : EntityBase
{
    private readonly DbSet<T> _dbSet;
    protected EmployeeContext _dbConext { get; set; }

    public GenericRepository(EmployeeContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
        _dbConext = dbContext;
    }
    public async Task AddAsync(T entity)
    {
        await _dbConext.AddAsync(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        if (_dbConext.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        await Task.Run(() =>
        {
            _dbSet.Remove(entity);
        });
    }

    public async Task DeleteAsync(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (_dbConext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
        }

        await Task.Run(() =>
        {
            _dbSet.RemoveRange(entities);
        });
    }

    public Task<PagedList<T>> GetAllAsync(PageParams param)
    {
        return PagedList<T>.ToPagedList(_dbSet,
            param._pageNumber,
            param.PageSize);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.FirstOrDefaultAsync(expression);
    }

    public Task<T> GetByIdAsync(Guid id, string?[] includes = null)
    {
        includes = includes ?? new string[0];

        var entity = _dbSet.AsNoTracking()
            .Where(x => x.Id == id)
            .IncludeMultiple(includes)
            .FirstOrDefaultAsync();
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
            if (GuidExtensions.IsNullOrEmptyGuid(entity.Id) == true)
            {
                await _dbSet.AddAsync(entity);
            }
            else
            {
                _dbSet.Update(entity);
            }
        }
    }

    public async Task UpdateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }
}
