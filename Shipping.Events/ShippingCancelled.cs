using NServiceBus;

namespace Shipping.Events
{
    public class ShippingCancelled : IEvent
    {
        public int OrderId { get; set; }
    }
}