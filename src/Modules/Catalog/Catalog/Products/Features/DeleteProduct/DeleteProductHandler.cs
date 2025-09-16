

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductHandler (CatalogDbContext catalogDbContext)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var productInDb = await catalogDbContext
                    .Products
                    .FindAsync([command.Id], cancellationToken: cancellationToken);

            if (productInDb is null)
            {
                throw new Exception($"Product not found:{command.Id} ");
            }

            catalogDbContext.Products.Remove(productInDb);
            await catalogDbContext.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(IsSuccess:true);
        }
    }
}
