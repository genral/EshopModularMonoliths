using Basket.Basket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace Basket.Data.Configuration
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(x=>x.Id); 

            builder.HasIndex(x=>x.UserName)
                .IsUnique();

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey(si => si.ShoppingCartId);
        }
    }
}
