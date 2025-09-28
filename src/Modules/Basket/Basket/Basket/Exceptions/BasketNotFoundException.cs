using Shared.Exceptions; 
namespace Basket.Basket.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string userName) : base("ShoppingCart", userName)
        {
        }
        public BasketNotFoundException(Guid id) : base("ShoppingCart", id)
        {
        }
    }
}
