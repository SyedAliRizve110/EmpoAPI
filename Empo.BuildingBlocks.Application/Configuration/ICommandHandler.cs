using Empo.BuildingBlocks.Application.Contracts;
using MediatR;

namespace Empo.BuildingBlocks.Application.Configuration;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
{

}

public interface ICommandHandler<in TCommand, TResult> :
    IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{

}