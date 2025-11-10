using Empo.BuildingBlocks.Domain.Interfaces;

namespace Empo.BuildingBlocks.Domain.Base;

public class DomainEventBase : IDomainEvent
{
    public Guid Id { get; }

    public DateTime OccurredOn { get; }

    public DomainEventBase()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }
}