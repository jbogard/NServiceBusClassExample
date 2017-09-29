using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Pricing.Events;

namespace Sales
{
    public class ProductPricingUpdatedHandler : IHandleMessages<ProductPricingUpdated>
    {
        public Task Handle(ProductPricingUpdated message, IMessageHandlerContext context)
        {
            LogManager.GetLogger(GetType()).Info("Product pricing updated");
            return Task.CompletedTask;
        }
    }
}