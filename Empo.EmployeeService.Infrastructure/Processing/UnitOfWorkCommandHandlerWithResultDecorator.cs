using Empo.BuildingBlocks.Application.Commands;
using Empo.BuildingBlocks.Application.Configuration.Commands;
using Empo.BuildingBlocks.Application.Contracts;
using Empo.BuildingBlocks.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Processing;

public class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : ICommandHandler<T, TResult> where T : ICommand<TResult>
{
    private readonly ICommandHandler<T, TResult> _decorated;

    private readonly IUnitOfWork _unitOfWork;

    private readonly EmployeeContext _Context;

    public UnitOfWorkCommandHandlerWithResultDecorator(
        ICommandHandler<T, TResult> decorated,
        IUnitOfWork unitOfWork,
        EmployeeContext Context)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        _Context = Context;
    }

    public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
    {
        var result = await this._decorated.Handle(command, cancellationToken);

        if (command is InternalCommandBase<TResult>)
        {
            var internalCommand = await _Context.InternalCommand.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

            if (internalCommand != null)
            {
                internalCommand.ProcessedDate = DateTime.UtcNow;
            }
        }

        await this._unitOfWork.CommitAsync(cancellationToken);

        return result;
    }
}