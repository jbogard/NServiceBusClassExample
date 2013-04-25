using NServiceBus;

namespace Shipping.Events
{
    public class ProductReturned : IEvent
    {
        public int OrderId { get; set; }
    }
}