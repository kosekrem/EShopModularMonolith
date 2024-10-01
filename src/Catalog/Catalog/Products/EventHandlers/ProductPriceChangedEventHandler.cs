using MassTransit;
using Shared.Messaging.Events;

namespace Catalog.Products.EventHandlers;

public class ProductPriceChangedEventHandler
    (ILogger<ProductPriceChangedEventHandler> logger, IBus bus)
    : INotificationHandler<ProductPriceChangedEvent>
{
    public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
        
        // publish product price changed integration event for update basket price.
        var integrationEvent = new ProductPriceChangedIntegrationEvent
        {
            ProductId = notification.Product.Id,
            Name = notification.Product.Name,
            Category = notification.Product.Category,
            Description = notification.Product.Description,
            ImageFileName = notification.Product.ImageFile,
            Price = notification.Product.Price
        };
        
        await bus.Publish(integrationEvent, cancellationToken);
    }
}