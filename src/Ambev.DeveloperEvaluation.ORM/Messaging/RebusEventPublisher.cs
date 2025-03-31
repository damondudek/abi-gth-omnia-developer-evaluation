using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Infrastructure.Messaging;

/// <summary>
/// Rebus implementation of IEventPublisher using RabbitMQ as transport
/// </summary>
public class RebusEventPublisher : IEventPublisher
{
    private readonly IBus _bus;

    public RebusEventPublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishAsync(IDomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case SaleCreatedEvent created:
                await _bus.Publish(created);
                break;
            case SaleModifiedEvent modified:
                await _bus.Publish(modified);
                break;
            case SaleCancelledEvent cancelled:
                await _bus.Publish(cancelled);
                break;
            case ItemCancelledEvent itemCancelled:
                await _bus.Publish(itemCancelled);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(domainEvent));
        }
    }
}