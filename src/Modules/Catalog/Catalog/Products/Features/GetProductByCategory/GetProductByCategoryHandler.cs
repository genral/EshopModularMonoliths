
using Catalog.Products.Dtos;

namespace Catalog.Products.Features.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category):IQuery<GetProcuctByCategoryResult>;

    public record GetProcuctByCategoryResult(IEnumerable<ProductDto> Products);

    public class GetProductByCategoryHandler(CatalogDbContext catalogDbContext)
        : IQueryHandler<GetProductByCategoryQuery, GetProcuctByCategoryResult>
    {
        public async Task<GetProcuctByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await catalogDbContext.Products
                .AsNoTracking()
                .Where(p=>p.Category.Contains(query.Category))
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            //var productsDto= ProjectToProductDto(products);
            var productsDto = products.Adapt<IEnumerable<ProductDto>>();

            return new GetProcuctByCategoryResult(productsDto);
        }
    }
}
