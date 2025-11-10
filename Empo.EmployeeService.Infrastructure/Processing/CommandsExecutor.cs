using Autofac;
using Empo.BuildingBlocks.Application.Contracts;
using MediatR;

namespace Empo.EmployeeService.Infrastructure.Processing;
public static class CommandsExecutor
{
    public static async Task Execute(ICommand command)
    {
        using (var scope = CompositionRoot.BeginLifeTimeScope())
        {
            var mediaor = scope.Resolve<IMediator>();
            await mediaor.Send(command);
        }
    }
    public static async Task<TResult> Execute<TResult>(ICommand<TResult> command)
    {
        using (var scope = CompositionRoot.BeginLifeTimeScope())
        {
            var mediator = scope.Resolve<IMediator>();
            return await mediator.Send(command);
        }
    }
}

