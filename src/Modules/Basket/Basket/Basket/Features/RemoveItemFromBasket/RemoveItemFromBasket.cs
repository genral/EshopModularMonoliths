

using Basket.Basket.Exceptions;
using Basket.Data;
using Shared.CQRS;

namespace Basket.Basket.Features.RemoveItemFromBasket
{
    public record RemoveItemFromBasketCommand(string UserName, Guid ProductId):ICommand<RemoveItemFromBasketResult>;
    public record RemoveItemFromBasketResult(Guid Id);

    public class RemoveItemFromBasket(BasketDbContext basketDbContext)
        : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
    {
        public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await basketDbContext.ShoppingCarts
                .Include(x => x.Items)
                .SingleOrDefaultAsync(s => s.UserName == command.UserName, cancellationToken);

            if (basket is null)
            {
                throw new BasketNotFoundException(command.UserName);
            }

            basket.RemoveItem(command.ProductId);

            await basketDbContext.SaveChangesAsync();

            return new RemoveItemFromBasketResult(basket.Id);
        }
    }
}
