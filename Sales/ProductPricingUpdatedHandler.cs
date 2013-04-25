using NServiceBus;
using NServiceBus.Logging;
using Pricing.Events;

namespace Sales
{
    public class ProductPricingUpdatedHandler : IHandleMessages<ProductPricingUpdated>
    {
        public void Handle(ProductPricingUpdated message)
        {
            LogManager.GetLogger(GetType()).Info("Product pricing updated");
        }
    }
}