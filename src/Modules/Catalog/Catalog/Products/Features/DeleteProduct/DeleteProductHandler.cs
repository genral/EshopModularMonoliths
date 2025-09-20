
using Catalog.Products.Exceptions;

namespace Catalog.Products.Features.DeleteProduct
{
    public record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Product id is required");
        }
    }

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
                throw new ProductNotFoundException(command.Id); 
            }

            catalogDbContext.Products.Remove(productInDb);
            await catalogDbContext.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(IsSuccess:true);
        }
    }
}
