 
using Basket.Basket.Exceptions;
using Basket.Data.Repository;
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

    public class AddItemIntoBasketHandler (IBasketRepository basketRepository)
        : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
    {
        public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
        {
             

            var basket=await basketRepository.GetBasket(command.UserName, false, cancellationToken);

            basket.AddItem(command.ShoppingCartItem.ProductId,
                command.ShoppingCartItem.Quantity,
                command.ShoppingCartItem.Price,
                command.ShoppingCartItem.Color,
                command.ShoppingCartItem.ProductName);

            await basketRepository.SaveChangesAsync(command.UserName, cancellationToken);

            return new AddItemIntoBasketResult(basket.Id);
        }
    }
}
