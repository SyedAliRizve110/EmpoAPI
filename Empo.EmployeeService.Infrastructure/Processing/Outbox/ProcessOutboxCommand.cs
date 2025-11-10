using Empo.BuildingBlocks.Application.Contracts;
using MediatR;

namespace Empo.EmployeeService.Infrastructure.Processing.Outbox;

public class ProcessOutboxCommand : CommandBase<Unit>, IRecurringCommand
{

}