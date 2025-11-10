using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Infrastructure.Processing.Outbox;
using MediatR;

namespace Empo.EmployeeService.Infrastructure.Processing.InternalCommands;

internal class ProcessInternalCommandsCommand : CommandBase<Unit>, IRecurringCommand
{

}