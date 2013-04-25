using NServiceBus;

namespace Billing.Events
{
    public class OrderBilled : IEvent
    {
        public int OrderId { get; set; }
    }
}