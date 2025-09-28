

using MassTransit;
using Shared.Messaging.Events;

namespace Catalog.Products.EventHandler
{
    public class ProductPriceChangedEventHandler (ILogger<ProductPriceChangedEventHandler> logger, IBus bus)
        : INotificationHandler<ProductPriceEvent>
    {
        public async Task Handle(ProductPriceEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);

            //publish product price changed Integration Event 
            var intergrationEvent=new  ProductPriceChangedIntegrationEvent {
                ProductId=notification.Product.Id,
                Category=notification.Product.Category,
                Price=notification.Product.Price,
                Description=notification.Product.Description,
                ImageFile=notification.Product.ImageFile,
                Name = notification.Product.Name
            };

            await bus.Publish(intergrationEvent, cancellationToken);
             
        }
    }
}
