using Empo.BuildingBlocks.Application.Contracts;
using MediatR;

namespace Empo.BuildingBlocks.Application.Configuration.Queries;

public interface IQueryHandlerBase<in TQuery, TResult> :
    IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
{
}
