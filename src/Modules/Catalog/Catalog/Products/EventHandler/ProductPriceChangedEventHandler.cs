

namespace Catalog.Products.EventHandler
{
    public class ProductPriceChangedEventHandler (ILogger<ProductPriceChangedEventHandler> logger)
        : INotificationHandler<ProductPriceEvent>
    {
        public Task Handle(ProductPriceEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event Handled: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
