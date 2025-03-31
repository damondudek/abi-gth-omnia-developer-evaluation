using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public interface IDomainEvent {}
    public record SaleCreatedEvent(Sale Sale) : IDomainEvent;
    public record SaleModifiedEvent(Sale Sale) : IDomainEvent;
    public record SaleCancelledEvent(Sale Sale) : IDomainEvent;
    public record ItemCancelledEvent(Guid ItemId, Guid SaleId) : IDomainEvent;
}
