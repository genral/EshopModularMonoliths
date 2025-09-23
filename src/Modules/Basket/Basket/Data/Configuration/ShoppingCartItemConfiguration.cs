using Basket.Basket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Data.Configuration
{
    public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductId).IsRequired();

            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x=>x.Color);

            builder.Property(x=>x.ProductName).IsRequired();

            builder.Property(x=>x.Price).IsRequired();
        }
    }
}
