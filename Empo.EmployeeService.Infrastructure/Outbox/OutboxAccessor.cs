using Empo.BuildingBlocks.Application.Outbox;
using Empo.EmployeeService.Infrastructure;

namespace Empo.EmployeeService.Infrastructure.Outbox;

internal class OutboxAccessor : IOutbox
{
	private readonly EmployeeContext _context;

	internal OutboxAccessor(EmployeeContext context)
	{
		_context = context;
	}

	public void Add(OutboxMessage message)
	{
		_context.OutboxMessage.Add(message);
	}

	public Task Save()
	{
		return Task.CompletedTask; // Save is done automatically using EF Core Change Tracking mechanism during SaveChanges.
	}
}
