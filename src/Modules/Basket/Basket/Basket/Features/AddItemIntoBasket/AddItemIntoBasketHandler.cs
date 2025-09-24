 
using Basket.Basket.Exceptions; 
using FluentValidation;
using Shared.CQRS; 

namespace Basket.Basket.Features.AddItemIntoBasket
{
    public record AddItemIntoBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItem):ICommand<AddItemIntoBasketResult>;
    public record AddItemIntoBasketResult(Guid Id);

    public class AddItemBasketValidator : AbstractValidator<AddItemIntoBasketCommand>
    {
        public AddItemBasketValidator()
        {
            RuleFor(x=>x.UserName).NotEmpty().WithMessage("UserName is Required");
            RuleFor(x => x.ShoppingCartItem.ProductId).NotEmpty().WithMessage("ProductId is Required");
            RuleFor(x => x.ShoppingCartItem.Quantity).GreaterThan(0).WithMessage("Quantity should be greater than zero");
        }
    }

    public class AddItemIntoBasketHandler (BasketDbContext basketDbContext)
        : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
    {
        public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await basketDbContext.ShoppingCarts 
                .Include(x => x.Items)
                .SingleOrDefaultAsync(s => s.UserName == command.UserName, cancellationToken);

            if (basket is null)
            {
                throw new BasketNotFoundException(command.UserName);
            }

            basket.AddItem(command.ShoppingCartItem.ProductId,
                command.ShoppingCartItem.Quantity,
                command.ShoppingCartItem.Price,
                command.ShoppingCartItem.Color,
                command.ShoppingCartItem.ProductName);

            await basketDbContext.SaveChangesAsync(cancellationToken);

            return new AddItemIntoBasketResult(basket.Id);
        }
    }
}
