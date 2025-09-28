﻿

using Basket.Basket.Features.UpdateItemPriceInBasket;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messaging.Events;

namespace Basket.Basket.EventHandlers
{
    public class ProductPriceChangedINtegrationEventHandler(ISender sender, ILogger<ProductPriceChangedINtegrationEventHandler> logger) 
        : IConsumer<ProductPriceChangedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
        {
            logger.LogInformation("Intergration Event Handeled: {IntegrationEvent}", context.Message.GetType().Name);

            var command = new UpdateItemPriceInBasketCommand(context.Message.ProductId, context.Message.Price);

            var result= await sender.Send(command);

            if(!result.IsSuccess)
            {
                logger.LogError("Error updating price in basket for product id: {ProductId}", context.Message.ProductId);
            }


            logger.LogInformation("Price for product id: {ProductId} updated in basket", context.Message.ProductId);
        }
    }
}
