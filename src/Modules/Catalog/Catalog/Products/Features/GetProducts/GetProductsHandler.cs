
using Catalog.Products.Dtos;

namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery():IQuery<GetProductsResult>;

    public record GetProductsResult(IEnumerable<ProductDto> Products);

    public class GetProductsHandler (CatalogDbContext catalogDbContext)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products= await catalogDbContext.Products
                .AsNoTracking()
                .OrderBy(p=>p.Name)
                .ToListAsync(cancellationToken);

            //var productsDto= ProjectToProductDto(products);
            var productsDto= products.Adapt<IEnumerable<ProductDto>>(); 

            return new GetProductsResult(productsDto);

        }

        private List<ProductDto> ProjectToProductDto(List<Product> products)
        {
            foreach (var product in products)
            {

            }
            return [];
        }
    }
}
