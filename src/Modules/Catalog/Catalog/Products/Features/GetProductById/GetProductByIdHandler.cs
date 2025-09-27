

using Catalog.Products.Exceptions; 

namespace Catalog.Products.Features.GetProductById
{

    //public record GetProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;

    //public record GetProductByIdResult(ProductDto Product);

    public class GetProductByIdHandler (CatalogDbContext catalogDbContext)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await catalogDbContext.Products 
                .FindAsync([query.Id], cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException(query.Id);
            }

            var productDto=product.Adapt<ProductDto>();

            return new GetProductByIdResult (productDto);

        }
    }
}
