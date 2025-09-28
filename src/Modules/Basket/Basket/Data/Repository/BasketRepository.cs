

using Basket.Basket.Exceptions;

namespace Basket.Data.Repository
{
    public class BasketRepository (BasketDbContext basketDbContext)
        : IBasketRepository
    {
        public async Task<ShoppingCart> CreateBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            basketDbContext.ShoppingCarts.Add(basket); 
            await basketDbContext.SaveChangesAsync(cancellationToken);

            return basket;
            
        }

        public async Task<bool> DeletBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basket=await GetBasket(userName, false);
            basketDbContext.ShoppingCarts.Remove(basket);
            await basketDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            var query = basketDbContext.ShoppingCarts
                            .Include(s => s.Items)
                            .Where(s => s.UserName == userName);

            if(asNoTracking) query.AsNoTracking();

            var basket=await query.SingleOrDefaultAsync(cancellationToken);

            return basket ?? throw new BasketNotFoundException(userName);
        }

        public async Task<IList<ShoppingCartItem>> GetBasketItems(Guid productId, bool asNoTracking = true, CancellationToken cancellationToken = default)
        {
            var query =  basketDbContext.ShoppingCartItems
                .Where(i => i.ProductId == productId);

            if(asNoTracking)
            {
                query.AsNoTracking();   
            }

            return await query.ToListAsync();  

        }

        public async Task<int> SaveChangesAsync(string? userName = null, CancellationToken cancellationToken = default)
        {
            return await basketDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
