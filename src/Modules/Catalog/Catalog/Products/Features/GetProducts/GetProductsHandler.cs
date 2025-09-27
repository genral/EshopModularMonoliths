 
using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts
{
    public record GetProductsQuery(PaginationRequest PaginationRequest) :IQuery<GetProductsResult>;

    public record GetProductsResult(PaginatedResult<ProductDto> Products);

    public class GetProductsHandler (CatalogDbContext catalogDbContext)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {

            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await catalogDbContext.Products.LongCountAsync(cancellationToken);

            var products= await catalogDbContext.Products
                .AsNoTracking()
                .OrderBy(p=>p.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            //var productsDto= ProjectToProductDto(products);
            var productsDto= products.Adapt<IEnumerable<ProductDto>>(); 

            return new GetProductsResult(new PaginatedResult<ProductDto>
            (
                pageIndex,
                pageSize,
                totalCount,
                productsDto 
            ));

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
