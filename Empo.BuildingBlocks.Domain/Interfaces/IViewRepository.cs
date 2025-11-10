using Empo.BuildingBlocks.Domain.Base;
using System.Linq.Expressions;

namespace Empo.BuildingBlocks.Domain.Interfaces;

public interface IViewRepository<T> where T : ViewBase
{
    Task<List<T>> List(Expression<Func<T, bool>> expression);
    IQueryable<T> View { get; }
    Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);
}
