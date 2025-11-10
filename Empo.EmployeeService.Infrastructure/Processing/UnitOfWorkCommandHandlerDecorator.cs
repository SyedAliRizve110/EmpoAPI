using Empo.BuildingBlocks.Application.Commands;
using Empo.BuildingBlocks.Application.Configuration.Commands;
using Empo.BuildingBlocks.Application.Contracts;
using Empo.BuildingBlocks.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Processing;

public class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
{
    private readonly ICommandHandler<T> _decorated;

    private readonly IUnitOfWork _unitOfWork;

    private readonly EmployeeContext _Context;

    public UnitOfWorkCommandHandlerDecorator(
        ICommandHandler<T> decorated,
        IUnitOfWork unitOfWork,
        EmployeeContext Context)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        _Context = Context;
    }

    public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
    {
        await this._decorated.Handle(command, cancellationToken);

        if (command is InternalCommandBase)
        {
            var internalCommand =
                await _Context.InternalCommand.FirstOrDefaultAsync(x => x.Id == command.Id,
                    cancellationToken: cancellationToken);

            if (internalCommand != null)
            {
                internalCommand.ProcessedDate = DateTime.UtcNow;
            }
        }

        await this._unitOfWork.CommitAsync(cancellationToken);

        return Unit.Value;
    }

    Task IRequestHandler<T>.Handle(T request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);

    }
}
