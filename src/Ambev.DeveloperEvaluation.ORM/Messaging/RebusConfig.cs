using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace Ambev.DeveloperEvaluation.ORM.Messaging;

public static class RebusConfig
{
    public static IServiceCollection AddRebusWithRabbitMq(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetSection("RabbitMQ");
        var connectionString = rabbitMqSettings["ConnectionString"];
        var queueName = rabbitMqSettings["QueueName"];

        services.AddRebus(configure => configure
            .Transport(t => t.UseRabbitMq(connectionString, queueName))
            .Routing(r => r.TypeBased()
                .Map<SaleCreatedEvent>("sale.created")
                .Map<SaleModifiedEvent>("sale.modified")
                .Map<SaleCancelledEvent>("sale.cancelled")
                .Map<ItemCancelledEvent>("item.cancelled")));

        services.AutoRegisterHandlersFromAssemblyOf<SaleCreatedEvent>();

        return services;
    }
}
