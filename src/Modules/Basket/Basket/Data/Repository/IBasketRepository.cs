 
namespace Basket.Data.Repository
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName, bool asNoTracking=true, CancellationToken cancellationToken = default);
        Task<ShoppingCart> GetBasket(Guid id, bool asNoTracking = true, CancellationToken cancellationToken = default);
        Task<ShoppingCart> CreateBasket(ShoppingCart basket, CancellationToken cancellationToken = default);
        Task<bool> DeletBasket(string userName, CancellationToken cancellationToken = default); 
        Task<int> SaveChangesAsync(string? userName=null, CancellationToken cancellationToken = default);
        Task<IList<ShoppingCartItem>> GetBasketItems(Guid productId, bool asNoTracking = true, CancellationToken cancellationToken = default);
        Task<bool> UpdateItemPriceInBasket(Guid productId,decimal price, CancellationToken cancellationToken=default);
    }
}
