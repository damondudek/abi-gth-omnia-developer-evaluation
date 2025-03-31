using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;

namespace Ambev.DeveloperEvaluation.Infrastructure.Messaging;

public class SaleEventHandlers :
    IHandleMessages<SaleCreatedEvent>,
    IHandleMessages<SaleModifiedEvent>,
    IHandleMessages<SaleCancelledEvent>,
    IHandleMessages<ItemCancelledEvent>
{
    private readonly ILogger<SaleEventHandlers> _logger;

    public SaleEventHandlers(ILogger<SaleEventHandlers> logger)
    {
        _logger = logger;
    }

    public Task Handle(SaleCreatedEvent message)
    {
        _logger.LogInformation($"New sale created: {message.Sale.SaleNumber} for customer {message.Sale.CustomerId} with total {message.Sale.TotalAmount}");
        return Task.CompletedTask;
    }

    public Task Handle(SaleModifiedEvent message)
    {
        _logger.LogInformation($"Sale {message.Sale.SaleNumber} was modified at {message.Sale.UpdatedAt}");
        return Task.CompletedTask;
    }

    public Task Handle(SaleCancelledEvent message)
    {
        _logger.LogWarning($"Sale {message.Sale.SaleNumber} was cancelled.");
        return Task.CompletedTask;
    }

    public Task Handle(ItemCancelledEvent message)
    {
        _logger.LogInformation($"Item {message.ItemId} was cancelled in sale {message.SaleId}. Product: {message.SaleId}");
        return Task.CompletedTask;
    }
}