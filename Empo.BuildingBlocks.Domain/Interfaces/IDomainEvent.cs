using MediatR;

namespace Empo.BuildingBlocks.Domain.Interfaces;

public interface IDomainEvent : INotification
{
    Guid Id { get; }

    DateTime OccurredOn { get; }
}