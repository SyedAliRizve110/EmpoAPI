
using Empo.BuildingBlocks.Domain.Interfaces;

namespace Empo.BuildingBlocks.Infrastructure.DomainEventsDispatching;

public class DomainEventsAccessor : IDomainEventsAccessor
{
	//private readonly DbContext _Context;

	//private IDomainEventCollector _domainEventCollector;

	private List<IDomainEvent> _domainEvents;

	public DomainEventsAccessor()
	{
		_domainEvents ??= new List<IDomainEvent>();
	}

	public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
	{
		//var domainEntities = this._meetingsContext.ChangeTracker
		//    .Entries<DomainBase>()
		//    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

		//return domainEntities
		//    .SelectMany(x => x.Entity.DomainEvents)
		//    .ToList();

		return _domainEvents?.AsReadOnly();
	}

	public void ClearAllDomainEvents()
	{
		//var domainEntities = this._Context.ChangeTracker
		//    .Entries<DomainBase>()
		//    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

		_domainEvents = new List<IDomainEvent>();
	}

	public void AddDomainEvent(IDomainEvent domainEvent)
	{
		_domainEvents ??= new List<IDomainEvent>();
		_domainEvents.Add(domainEvent);
	}
}