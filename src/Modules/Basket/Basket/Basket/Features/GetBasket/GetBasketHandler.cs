 
using Basket.Data.Repository;
using FluentValidation;
 
namespace Basket.Basket.Features.GetBasket
{
    public record GetBasketQuery(string UserName):IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCartDto ShoppingCart);

    public class GetBasketQueryValidator : AbstractValidator<GetBasketQuery>
    {
        public GetBasketQueryValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is Required");
        }
    }

    public class GetBasketHandler (IBasketRepository basketRepository)
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
             
            var basket=await basketRepository.GetBasket(query.UserName,true,cancellationToken);

            var basketDto= basket.Adapt<ShoppingCartDto>();

            return new GetBasketResult(basketDto);
        }
    }
}
