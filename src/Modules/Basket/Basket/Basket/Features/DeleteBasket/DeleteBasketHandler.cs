using Basket.Basket.Exceptions;
using Basket.Data.Repository;
 
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


    public class DeleteBasketHandler(IBasketRepository basketRepository)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
             
            await basketRepository.DeletBasket(command.UserName, cancellationToken); 

            return new DeleteBasketResult(true);
        }
    }
}
