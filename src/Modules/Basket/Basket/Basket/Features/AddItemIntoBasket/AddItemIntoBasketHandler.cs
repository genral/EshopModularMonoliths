  
using Basket.Data.Repository;
using Catalog.Contracts.Products.Features.GetProductById;

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

    public class AddItemIntoBasketHandler (IBasketRepository basketRepository, ISender sender)
        : ICommandHandler<AddItemIntoBasketCommand, AddItemIntoBasketResult>
    {
        public async Task<AddItemIntoBasketResult> Handle(AddItemIntoBasketCommand command, CancellationToken cancellationToken)
        {
             

            var basket=await basketRepository.GetBasket(command.UserName, false, cancellationToken);

            //Get lastest product information and set Price and ProductName

            var result= await sender.Send(new GetProductByIdQuery(command.ShoppingCartItem.ProductId));

            basket.AddItem(command.ShoppingCartItem.ProductId,
                command.ShoppingCartItem.Quantity,
                //command.ShoppingCartItem.Price,
                result.Product.Price,
                command.ShoppingCartItem.Color,
                result.Product.Name);
                //command.ShoppingCartItem.ProductName);

            await basketRepository.SaveChangesAsync(command.UserName, cancellationToken);

            return new AddItemIntoBasketResult(basket.Id);
        }
    }
}
