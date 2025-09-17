 
namespace Catalog.Products.Features.UpdateProduct
{
    public record UpdateProductCommand(ProductDto Product):ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductHandler (CatalogDbContext catalogDbContext)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var productInDb= await catalogDbContext
                    .Products
                    .FindAsync([command.Product.Id], cancellationToken:cancellationToken);

            if (productInDb is null)
            {
                throw new Exception($"Product not found:{command.Product.Id} ");
            }
            UpdateProductWithNewValues(productInDb, command.Product);

            catalogDbContext.Products.Update(productInDb);
            await catalogDbContext.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(IsSuccess: true);
        }

        private void UpdateProductWithNewValues(Product productInDb, ProductDto productDto)
        {
            productInDb.Update(productDto.Name, productDto.Category, productDto.Description, productDto.ImageFile, productDto.Price);

        }
    }
}
