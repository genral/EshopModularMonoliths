using Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Basket.Models
{
    public class ShoppingCart:Aggregate<Guid>
    {
        public string UserName { get; private set; } = default!;
        private readonly List<ShoppingCartItem> _items = new();
        public IReadOnlyList<ShoppingCartItem> Items =>_items.AsReadOnly();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public void AddItem(Guid productId, int quantity, decimal price, string color, string productName)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var existingItm=Items.FirstOrDefault(x=>x.ProductId==productId);

            if (existingItm != null)
            {
                existingItm.Quantity += quantity;
            }
            else { 
                var newItem=new ShoppingCartItem(Id,productId, quantity, color,price,productName);
                _items.Add(newItem);
            }
        }
        public void RemoveItem(Guid productId) {
            var existingItm = Items.FirstOrDefault(x => x.ProductId == productId);

            if (existingItm != null)
            {
                _items.Remove(existingItm);
            }
        }

        public static ShoppingCart Create(Guid id, string userName)
        {
            ArgumentException.ThrowIfNullOrEmpty(userName);

            var shoppingCart = new ShoppingCart {
                Id = id,
                UserName = userName, 
            };

            return shoppingCart;
        }
    }
}
