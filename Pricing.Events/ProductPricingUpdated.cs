using NServiceBus;

namespace Pricing.Events
{
    public class ProductPricingUpdated : IEvent
    {
        public int ProductId { get; set; }
    }
}