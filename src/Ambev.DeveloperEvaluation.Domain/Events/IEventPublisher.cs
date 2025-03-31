namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public interface IEventPublisher
    {
        /// <summary>
        /// Publishes a domain event to the message bus
        /// </summary>
        /// <param name="domainEvent">The domain event to publish</param>
        /// <returns>Task representing the async operation</returns>
        Task PublishAsync(IDomainEvent domainEvent);
    }
}
