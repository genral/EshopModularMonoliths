
namespace Catalog.Products.Events
{
    public  record ProductPriceEvent(Product Product):IDomainEvent;
}
