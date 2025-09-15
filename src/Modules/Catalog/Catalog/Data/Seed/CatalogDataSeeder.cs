using Shared.Data.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Data.Seed
{
    public class CatalogDataSeeder (CatalogDbContext dbContext)
        : IDataSeeder
    {
        public async Task SeedAllAsync()
        {
            if(!dbContext.Products.Any())
            {
                await dbContext.Products.AddRangeAsync(InitialData.Products);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
