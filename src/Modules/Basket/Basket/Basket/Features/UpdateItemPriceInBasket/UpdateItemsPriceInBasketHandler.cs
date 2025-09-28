

using Basket.Data.Repository;

namespace Basket.Basket.Features.UpdateItemPriceInBasket
{

    public record UpdateItemPriceInBasketCommand(Guid ProductId, decimal Price):ICommand<UpdateItemPriceInBasketResult>;
    public record UpdateItemPriceInBasketResult(bool IsSuccess);

    public class UpdateItemPriceInBasket : AbstractValidator<UpdateItemPriceInBasketCommand>
    {
        public UpdateItemPriceInBasket()
        {
            RuleFor(x=>x.ProductId).NotEmpty().WithMessage("ProductId is Required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price shoud be greater than zero");
        }
    }

    public class UpdateItemsPriceInBasketHandler (IBasketRepository basketRepository)
        : ICommandHandler<UpdateItemPriceInBasketCommand, UpdateItemPriceInBasketResult>
    {
        public async Task<UpdateItemPriceInBasketResult> Handle(UpdateItemPriceInBasketCommand command, CancellationToken cancellationToken)
        {
            var itemsToUpdate = await
                basketRepository.GetBasketItems(command.ProductId, false, cancellationToken);

            if(!itemsToUpdate.Any())
            {
                return new UpdateItemPriceInBasketResult(false);
            }

            foreach (var item in itemsToUpdate)
            {
                item.UpdatePrice(command.Price); 
            }

            await basketRepository.SaveChangesAsync(null, cancellationToken); 
            return new UpdateItemPriceInBasketResult(true);
        }

    }
}
