using Autofac;
using Empo.BuildingBlocks.Application.Contracts;
using MediatR;

namespace Empo.EmployeeService.Infrastructure.Processing;

public static class QueriesExecutor
{
    public static async Task<TResult> Execute<TResult>(IQuery<TResult> query)
    {
        using (var scope = CompositionRoot.BeginLifeTimeScope())
        {
            var mediator = scope.Resolve<IMediator>();

            return await mediator.Send(query);
        }
    }
}
