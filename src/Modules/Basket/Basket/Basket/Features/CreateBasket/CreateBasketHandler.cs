 
using Basket.Data.Repository; 
 
namespace Basket.Basket.Features.CreateBasket
{
    public record CreateBasketCommand(ShoppingCartDto ShoppingCart):ICommand<CreateBasketResult>;
    public record CreateBasketResult(Guid Id);


    public class CreateBasketCommandValidatror : AbstractValidator<CreateBasketCommand>
    {
        public CreateBasketCommandValidatror()
        {
            RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is Required"); 
        }
    }

    public class CreateBasketHandler(IBasketRepository basketRepository)
        : ICommandHandler<CreateBasketCommand, CreateBasketResult>
    {
        public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
        {
            //Create basket entity
            var newBasket= CreateNewBasket(command.ShoppingCart);

            //save in database

            await basketRepository.CreateBasket(newBasket, cancellationToken); 

            //return result
            return new CreateBasketResult(newBasket.Id);
        }

        private ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCart)
        {
            var newBasket = ShoppingCart.Create(
                shoppingCart.Id,
                shoppingCart.UserName
                );

            shoppingCart.Items.ForEach(item =>
            {
                newBasket.AddItem(item.ProductId,
                    item.Quantity,
                    item.Price,
                    item.Color,
                    item.ProductName);
            });

            return newBasket;
        }
    }
}
