

using Basket.Basket.Exceptions;
using Basket.Data;
using Basket.Data.Repository;
using Shared.CQRS;

namespace Basket.Basket.Features.RemoveItemFromBasket
{
    public record RemoveItemFromBasketCommand(string UserName, Guid ProductId):ICommand<RemoveItemFromBasketResult>;
    public record RemoveItemFromBasketResult(Guid Id);

    public class RemoveItemFromBasket(IBasketRepository basketRepository)
        : ICommandHandler<RemoveItemFromBasketCommand, RemoveItemFromBasketResult>
    {
        public async Task<RemoveItemFromBasketResult> Handle(RemoveItemFromBasketCommand command, CancellationToken cancellationToken)
        {
             
            var basket=await basketRepository.GetBasket(command.UserName, false, cancellationToken);

            basket.RemoveItem(command.ProductId);

            await basketRepository.SaveChangesAsync(command.UserName, cancellationToken);

            return new RemoveItemFromBasketResult(basket.Id);
        }
    }
}
