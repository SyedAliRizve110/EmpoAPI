namespace Empo.BuildingBlocks.Domain.Interfaces;

public interface IDomainEventsAccessor
{
	void AddDomainEvent(IDomainEvent domainEvent);
	IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();

	void ClearAllDomainEvents();


}