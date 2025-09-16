
using Catalog.Products.Dtos;

namespace Catalog.Products.Features.GetProductById
{

    public record GetProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(ProductDto Product);

    public class GetProductByIdHandler (CatalogDbContext catalogDbContext)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await catalogDbContext.Products 
                .FindAsync([query.Id], cancellationToken);

            if (product == null)
            {
                throw new Exception($"Product not found:{query.Id} ");
            }

            var productDto=product.Adapt<ProductDto>();

            return new GetProductByIdResult (productDto);

        }
    }
}
