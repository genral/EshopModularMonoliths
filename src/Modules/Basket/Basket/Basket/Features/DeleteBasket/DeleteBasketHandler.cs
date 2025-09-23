using Basket.Basket.Exceptions;
using FluentValidation;
using Shared.CQRS;

namespace Basket.Basket.Features.DeleteBasket
{
    public record DeleteBasketCommand(string UserName):ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is Required");
        }
    }


    public class DeleteBasketHandler(BasketDbContext basketDbContext)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            var basket=await basketDbContext.ShoppingCarts
                .SingleOrDefaultAsync(s=>s.UserName==command.UserName, cancellationToken);

            if(basket is null)
            {
                throw new BasketNotFoundException(command.UserName);
            }

            basketDbContext.ShoppingCarts.Remove(basket);
            await basketDbContext.SaveChangesAsync();

            return new DeleteBasketResult(true);
        }
    }
}
