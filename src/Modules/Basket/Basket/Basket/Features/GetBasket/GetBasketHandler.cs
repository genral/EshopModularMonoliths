
using Basket.Basket.Dtos;
using Basket.Basket.Exceptions;
using FluentValidation;
using Mapster;
using Shared.CQRS; 

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

    public class GetBasketHandler (BasketDbContext basketDbContext)
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            var basket = await basketDbContext.ShoppingCarts
                .AsNoTracking()
                .Include(x => x.Items)
                .SingleOrDefaultAsync(s => s.UserName == query.UserName, cancellationToken);

            if (basket is null)
            {
                throw new BasketNotFoundException(query.UserName);
            }

            var basketDto= basket.Adapt<ShoppingCartDto>();

            return new GetBasketResult(basketDto);
        }
    }
}
